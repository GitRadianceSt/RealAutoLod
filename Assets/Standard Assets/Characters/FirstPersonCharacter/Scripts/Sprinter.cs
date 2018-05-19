using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Sprinter : MonoBehaviour {
public float stamina = 10;
public float maxStamina = 10;

public float meleeStaminaAmount = 2;

CharacterController cc;
public bool isRunning;
public bool canHit;
//public Slider StaminaSlider;

public AudioSource source;
 public AudioClip[] breaths;
 private AudioClip breathClip;
 public GameObject audioObject;


void Start () 
{
    cc = gameObject.GetComponent<CharacterController> ();
	//stamina -= GameObject.FindWithTag ("Crowbar").GetComponent<PlayRandomAnimationOnClick> ().crowbarStaminaHit;
		//canHit = true;

   	 source = audioObject.GetComponent<AudioSource>();

}

void SetRunning(bool isRunning)
{
    this.isRunning = isRunning;
}

void Update () {

		GameObject crowbar = GameObject.FindWithTag("Melee");
		PlayRandomAnimationOnClick Crowbar = crowbar.GetComponent<PlayRandomAnimationOnClick> ();
		//stamina -= crowbarStaminaHit;
	

		if (stamina < 2) 
		{
			canHit = false;
			StartCoroutine (AudioPlay());
		}
		if (stamina > 2) 
		{
			canHit = true;
			crowbar.GetComponent<PlayRandomAnimationOnClick> ().enabled = true;
		}


		if (Input.GetButtonDown ("Fire1")) 
		{

			if (canHit == true) 
			{
				stamina -= meleeStaminaAmount;
				if (stamina < 0) 
				{
					stamina = 0;
					SetRunning (false);
					crowbar.GetComponent<PlayRandomAnimationOnClick> ().enabled = false;

				}
			}
		}


if (Input.GetKeyDown (KeyCode.LeftShift))
        SetRunning (true);
if (Input.GetKeyUp (KeyCode.LeftShift))
        SetRunning (false);



if (isRunning) {
        stamina -= Time.deltaTime;
        //StaminaSlider.value = stamina;
        if (stamina < 0) {
            stamina = 0;
            SetRunning (false);
			crowbar.GetComponent<PlayRandomAnimationOnClick> ().enabled = false;
			
        }
    } else if (stamina < maxStamina) 
    {
        stamina += Time.deltaTime;
        //StaminaSlider.value = stamina;
    }
}

	IEnumerator AudioPlay()
	{
		yield return new WaitWhile (()=> source.isPlaying);
		if (stamina < 1) 
		{
			//source.Play ();

			int index = Random.Range(0, breaths.Length);
        	 breathClip = breaths[index];
        	 source.clip = breathClip;
         	source.Play();

		}
	}

}