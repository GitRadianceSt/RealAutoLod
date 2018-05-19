using UnityEngine;
using System.Collections;

public class play_sound_on_trigger : MonoBehaviour
{
    public bool activateTrigger = false;

    public GameObject textO;
    public GameObject Sound;


    void Start()
    {
        textO.SetActive(false);
        Sound.SetActive(false);
    }


    void Update()
    {

        if (activateTrigger && Input.GetKey(KeyCode.E))
        {
            textO.SetActive(false);
            Sound.SetActive(true);
            Destroy(this.gameObject);
        }
      

    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            textO.SetActive(true);
            activateTrigger = true;
        }

    }


    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            textO.SetActive(false);
            activateTrigger = false;
        }

    }
}