using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour 
{
    public Light light;
    public UnityEngine.UI.Text text;
    public GameObject loadingScreen;
    
    private float deltaTime = 0.0f;

    void Awake()
    {
        loadingScreen.SetActive(true);
    }

    void Start()
    {
        Application.targetFrameRate = 500;
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string textStr = string.Format("{0:0.} fps", fps);
        text.text = textStr;

        //if (Input.GetKeyUp(KeyCode.T)) StartCoroutine(TestImposterSpeed());
    }

    /*IEnumerator TestImposterSpeed()
    {
        ImposterManager.instance.active = false;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        float startTime = Time.time;

        ImposterManager.instance.active = true;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        Debug.Log("Imposter generation time: " + (Time.time - startTime));
    }*/

    public void ToogleShadow()
    {
        if (light.shadows == LightShadows.None) light.shadows = LightShadows.Soft;
        else light.shadows = LightShadows.None;
        ImposterManager.instance.castShadow = true;
    }

    public void ToogleImposterRendering()
    {
        ImposterManager.instance.active = !ImposterManager.instance.active;
    }
}
