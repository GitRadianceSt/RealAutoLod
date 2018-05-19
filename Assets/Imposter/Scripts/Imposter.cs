using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Imposter : MonoBehaviour 
{
    public enum ImposterLodMethod
    {
        Distance, ScreenSize
    }

    public int maxTextureSize = 256;
    [HideInInspector]
    public ImposterLodMethod lodMethod = ImposterLodMethod.Distance;
    public float maxDistance = 45f;
    public float angleTolerance = 2.5f;
    public float distanceTolerance = 15.0f;
    public float zOffset = 0.25f;
    public bool castShadow = false;
    public float shadowZOffset = 0.25f;
    public float maxShadowDistance = 1.0f;
    public int shadowDownSampling = 2;
    public bool dynamic = true;
    public float updateInterval = 1.0f;
    
    private float pixelSize = 0.0f;
    private int lastTextureSize = 0;
    private ImposterProxy proxy;
    private Vector3 lastCameraVector = Vector3.zero;
    private List<Renderer> renderers = new List<Renderer>();
    private Dictionary<Renderer, int> originalRenderLayers = new Dictionary<Renderer, int>();
    private bool forceUpdate = false;
    private float lastUpdateTime = 0.0f;
    private float timeOffset = 0.0f;

	// Use this for initialization
	void Start () 
    {
        if (ImposterManager.instance == null)
        {
            Debug.LogError("Can´t find ImposterManager!");
            return;
        }

        Init();
	}

    public void ForceUpdate()
    {
        forceUpdate = true;
    }

    public void Init()
    { 
        lastCameraVector = Vector3.zero;
        timeOffset = Random.Range(0.0f, updateInterval);

        if (!ImposterManager.instance.imposters.Contains(this))
        {
            ImposterManager.instance.imposters.Add(this);
        }

        if (proxy != null)
        {
            DestroyImmediate(proxy.gameObject);
        }

        extractRenderer();
        createProxy();
        deactivate();
    }

    private void extractRenderer()
    {
        renderers.Clear();

        if (GetComponent<Renderer>() != null)
        {
            renderers.Add(GetComponent<Renderer>());
        }

        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            renderers.Add(r);
        }

        foreach (Renderer r in renderers)
        {
            if (!originalRenderLayers.ContainsKey(r))
            {
                originalRenderLayers.Add(r, r.gameObject.layer);
            }
        }
    }

    private void createProxy()
    {
        GameObject proxyGo = (GameObject)Instantiate(ImposterManager.instance.proxyPrefab.gameObject, transform.position, transform.rotation);
        proxyGo.transform.SetParent(transform);
        proxyGo.transform.localScale = new Vector3(1.0f / transform.lossyScale.x, 1.0f / transform.lossyScale.y, 1.0f / transform.lossyScale.z);
        
        proxyGo.SetActive(true);
        proxy = proxyGo.GetComponent<ImposterProxy>();
        proxy.shadowZOffset = shadowZOffset;
        proxy.zOffset = zOffset;
        proxy.shadowDivider = 1 << shadowDownSampling;
        proxy.maxShadowDistance = maxShadowDistance;
        proxy.Init(renderers, castShadow && ImposterManager.instance.castShadow);
    }

    private void activate()
    {
        foreach (Renderer r in renderers)
        {
            r.enabled = false;
            r.gameObject.layer = ImposterManager.instance.imposterLayer;
        }

        proxy.gameObject.SetActive(true);
    }

    private void deactivate()
    {
        foreach (Renderer r in renderers)
        {
            r.enabled = true;

            int originalLayer = 0;
            originalRenderLayers.TryGetValue(r, out originalLayer);
            r.gameObject.layer = originalLayer;
        }

        proxy.gameObject.SetActive(false);
    }

	public void UpdateImposter ()
    {
        if (!gameObject.activeInHierarchy)
        {
            // Don't update if disabled
            return;
        }

        if (!ImposterManager.instance.active)
        {
            if (proxy.isActiveAndEnabled)
            {
                proxy.InvalidateTexture();
                deactivate();
            }
            return;
        }

        Vector3 cameraVector = proxy.bound.center - ImposterManager.instance.mainCamera.transform.position;
        bool isInRange = IsInRange();
        bool isInFrustrum = proxy.isVisible();
        bool needsUpdate = NeedsUpdate() || forceUpdate;

        if (isInRange || forceUpdate)
        {
            if (!proxy.isActiveAndEnabled && ImposterManager.instance.active)
            {
                activate();
            }

            if (isInFrustrum)
            {
                proxy.setVisibility(true);

                if (needsUpdate || 
                    proxy.IsTextureInvalid() || 
                    (Time.time + timeOffset) - lastUpdateTime > updateInterval && dynamic)
                {
                    forceUpdate = false;
                    fitTexture();
                    proxy.Render();
                    lastCameraVector = cameraVector;
                    lastUpdateTime = Time.time + timeOffset;
                }
            }
            else
            {
                bool isVisibleInCache = proxy.isVisibleInCache();

                proxy.setVisibility(false);

                if (isVisibleInCache && ImposterManager.instance.cachingBehaviour == ImposterManager.CachingBehaviour.preloadAndCacheInvisibleImposters)
                {
                    if (needsUpdate || proxy.IsTextureInvalid())
                    {
                        if(ImposterManager.instance.getPreloadLock(this))
                        {
                            forceUpdate = false;
                            fitTexture();
                            proxy.Render();
                            lastCameraVector = cameraVector;
                            lastUpdateTime = Time.time;
                        }
                    }
                }
                else if (ImposterManager.instance.cachingBehaviour == ImposterManager.CachingBehaviour.discardInvisibleImpostors || needsUpdate || !isVisibleInCache)
                {
                    proxy.InvalidateTexture();
                }
            }
        }
        else
        {
            if (proxy.isActiveAndEnabled || !ImposterManager.instance.active && proxy.isActiveAndEnabled)
            {
                proxy.InvalidateTexture();
                deactivate();
                return;
            }
        }
	}

    private bool NeedsUpdate()
    {
        Vector3 cameraVector = proxy.bound.center - ImposterManager.instance.mainCamera.transform.position;
        float diff = Vector3.Angle(cameraVector, lastCameraVector);
        float distDiff = Mathf.Abs(cameraVector.magnitude - lastCameraVector.magnitude);

        if (diff > angleTolerance ||
            distDiff > lastCameraVector.magnitude * (distanceTolerance / 100.0f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsInRange()
    {
        bool isInRange = false;

        float cameraDistance = Vector3.Distance(ImposterManager.instance.mainCamera.transform.position, proxy.bound.center);
        float angularSize = (proxy.maxSize / cameraDistance) * Mathf.Rad2Deg;
        pixelSize = ((angularSize * Screen.height) / ImposterManager.instance.mainCamera.fieldOfView);

        if (lodMethod == ImposterLodMethod.Distance)
        {
            isInRange = cameraDistance > maxDistance;
        }
        else
        {
            isInRange = pixelSize < maxTextureSize;
        }
        return isInRange;
    }

    private void fitTexture()
    {
        int textureSize = Mathf.Min(maxTextureSize, nlpo2((int)pixelSize));

        if (proxy.IsTextureInvalid() || lastTextureSize != textureSize)
        {
            proxy.AdjustTextureSize(textureSize);
        }

        lastTextureSize = textureSize;
    }

    private int nlpo2(int x)
    {
        x--;
        x |= (x >> 1);
        x |= (x >> 2);
        x |= (x >> 4);
        x |= (x >> 8);
        x |= (x >> 16);

        return (x+1);
    }
}
