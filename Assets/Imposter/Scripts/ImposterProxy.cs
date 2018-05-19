using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ImposterProxy : MonoBehaviour 
{
    [HideInInspector]
    public float maxSize = 0.0f;
    [HideInInspector]
    public Bounds bound, shadowBound;
    [HideInInspector]
    public float shadowZOffset = 0.35f;
    [HideInInspector]
    public float zOffset = 0.35f;
    [HideInInspector]
    public float maxShadowDistance = 1.0f;
    [HideInInspector]
    public int shadowDivider = 1;
    public MeshRenderer quad, shadow;
    
    private ImposterTexture imposterTexture, shadowTexture;
    private Camera renderingCamera;
    private List<Renderer> renderers;
    private bool castShadow = true;

    void Awake()
    {
        imposterTexture = shadowTexture = null;
    }

	public void Init (List<Renderer> renderers, bool useShadow) 
    {
        if (imposterTexture != null) ImposterManager.instance.giveBackRenderTexture(imposterTexture);
        if (shadowTexture != null) ImposterManager.instance.giveBackRenderTexture(shadowTexture);

        this.castShadow = useShadow;
        this.renderers = renderers;
        renderingCamera = ImposterManager.instance.imposterRenderingCamera;
        imposterTexture = shadowTexture = null;
        if (!useShadow) shadow.gameObject.SetActive(false);

        extractBounds();
        adjustSize();
	}

    void OnDestroy()
    {
        InvalidateTexture();
    }

    public void AdjustTextureSize(int size)
    {
        if (imposterTexture != null && imposterTexture.size == size)
            return;

        if (imposterTexture != null)
        {
            ImposterManager.instance.giveBackRenderTexture(imposterTexture);
            if (castShadow) ImposterManager.instance.giveBackRenderTexture(shadowTexture);
        }

        imposterTexture = ImposterManager.instance.getRenderTexture(this, size);
        quad.material.mainTexture = imposterTexture.texture;
        quad.material.SetFloat("_ZOffset", zOffset);

        if (castShadow)
        {
            shadow.gameObject.SetActive(true);
            updateShadow(size);
        }
        else
        {
            shadow.gameObject.SetActive(false);
        }
    }

    private void updateShadow(int size)
    {
        shadowTexture = ImposterManager.instance.getRenderTexture(this, size / shadowDivider);
        shadow.material.mainTexture = shadowTexture.texture;

        shadow.transform.rotation = Quaternion.LookRotation(ImposterManager.instance.mainLight.transform.forward, Vector3.up);
        shadow.transform.position = transform.position - shadow.transform.forward * (shadowZOffset * maxSize / 2.0f);

        renderShadow();
    }

    private void extractBounds()
    {
        if (transform.parent.GetComponent<Renderer>() != null)
        {
            bound = transform.parent.GetComponent<Renderer>().bounds;
        }

        foreach (Renderer r in transform.parent.gameObject.GetComponentsInChildren<Renderer>())
        {
            if (r != GetComponentInChildren<Renderer>())
            {
                if (bound.extents == Vector3.zero) bound = r.bounds;
                else bound.Encapsulate(r.bounds);
            }
        }

        shadowBound = bound;
        shadowBound.extents *= (1.0f + maxShadowDistance);

        maxSize = bound.extents.magnitude * 2.0f;
    }

    public void Render()
    {
        quad.transform.rotation = Quaternion.LookRotation(transform.position - ImposterManager.instance.mainCamera.transform.position, Vector3.up);

        renderImposter();
    }

    public void InvalidateTexture()
    {
        if (IsTextureInvalid())
        {
            return;
        }

        ImposterManager.instance.giveBackRenderTexture(imposterTexture);
        imposterTexture = null;

        if (castShadow)
        {
            ImposterManager.instance.giveBackRenderTexture(shadowTexture);
            shadowTexture = null;
        }
    }

    public bool IsTextureInvalid()
    {
        if (imposterTexture == null || imposterTexture.texture == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void adjustSize()
    {
        transform.position = bound.center;
        Transform oldParent = transform.parent;
        transform.SetParent(null);
        transform.localScale = new Vector3(maxSize, maxSize, maxSize);
        transform.SetParent(oldParent);
    }

    private void renderImposter()
    {
        if (imposterTexture == null)
        {
            return;
        }

        renderingCamera.transform.position = ImposterManager.instance.mainCamera.transform.position;
        renderingCamera.transform.LookAt(bound.center);
        renderingCamera.cullingMask = 1 << ImposterManager.instance.imposterLayer;
        Vector3 cameraDirection = ImposterManager.instance.mainCamera.transform.position - bound.center;
        renderingCamera.orthographic = false;

        float dist = Vector3.Distance(ImposterManager.instance.mainCamera.transform.position, transform.position);
        float fov = 2.0f * Mathf.Atan(maxSize / (2.0f * dist)) * (180 / Mathf.PI);
        renderingCamera.fieldOfView = fov;
        renderingCamera.nearClipPlane = cameraDirection.magnitude - maxSize;
        renderingCamera.farClipPlane = cameraDirection.magnitude + maxSize;

        renderingCamera.targetTexture = imposterTexture.texture;
        foreach (Renderer r in renderers) r.enabled = true;

        renderingCamera.Render();

        foreach (Renderer r in renderers) r.enabled = false;
        renderingCamera.targetTexture = null;
    }

    private void renderShadow()
    {
        if (shadowTexture == null)
        {
            return;
        }

        Vector3 shadowDirection = ImposterManager.instance.mainLight.transform.forward;

        renderingCamera.transform.position = transform.position - shadowDirection * (2.0f * maxSize + 1.0f);
        renderingCamera.transform.LookAt(bound.center);
        renderingCamera.cullingMask = 1 << ImposterManager.instance.imposterLayer;
        
        renderingCamera.orthographic = true;
        renderingCamera.orthographicSize = maxSize / 2.0f;

        renderingCamera.nearClipPlane = 1.0f;
        renderingCamera.farClipPlane = 4.0f * maxSize + 1.0f;

        renderingCamera.targetTexture = shadowTexture.texture;
        foreach (Renderer r in renderers) r.enabled = true;

        renderingCamera.Render();

        foreach (Renderer r in renderers) r.enabled = false;
        renderingCamera.targetTexture = null;
    }

    public bool isVisible()
    {
        return castShadow ? ImposterManager.instance.isVisible(shadowBound) : ImposterManager.instance.isVisible(bound);
    }

    public bool isVisibleInCache()
    {
        return castShadow ? ImposterManager.instance.isVisibleInCache(shadowBound) : ImposterManager.instance.isVisibleInCache(bound);
    }

    public void setVisibility(bool bVisible)
    {
        if (bVisible && !quad.enabled)
        {
            quad.enabled = true;
            if(castShadow) shadow.enabled = true;
        }
        else if (!bVisible && quad.enabled)
        {
            quad.enabled = false;
            shadow.enabled = false;
        }
    }
}
