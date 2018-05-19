using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DemoSceneCreator : MonoBehaviour 
{
    public List<GameObject> prefabs = new List<GameObject>();
    public float objectSize = 10.0f;
    public int axisSize = 10;
    public UI ui;

	// Use this for initialization
	void Start () 
    {
        CreateObjects(axisSize);
	}

    public void CreateObjects(int count)
    {
        StartCoroutine(createObjectsAsync(count));
    }

    IEnumerator createObjectsAsync(int count)
    {

        ui.loadingScreen.SetActive(true);
        yield return new WaitForEndOfFrame();

        this.axisSize = count;
        Random.seed = 0;

        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        ImposterManager.instance.garbageCollect();

        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < count; j++)
            {
                int r = Random.Range(0, prefabs.Count);
                Vector3 randXY = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));

                GameObject go = (GameObject)Instantiate(prefabs[r], new Vector3((float)i * objectSize, 0.0f, (float)j * objectSize)
                    + randXY, Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f));
                go.transform.SetParent(transform);
            }
        }

        yield return new WaitForEndOfFrame();
        ui.loadingScreen.SetActive(false);
    }

    public void Reset()
    {
        CreateObjects(axisSize);
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
