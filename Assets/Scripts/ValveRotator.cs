using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveRotator : MonoBehaviour {

	 public float smooth = 1f;
   	 public Quaternion targetRotation;
   	 //public float rotationAmount = 30f;
   	 public float rotationTime = 20;

     public AudioClip[] valveSounds;
     public AudioSource audioSource;
     private AudioClip valveSoundClip;
     public AudioClip lockSound;

     public float minimumRotation = -90f;
	 public float maximumRotation = 90f;

	 public bool canTurnClockwise;
	 public bool canTurnCounterClockwise;

	 public bool lockSoundPlay = true;

	// Use this for initialization
	void Start () 
	{
		targetRotation = transform.rotation;
		//lockSoundPlay = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation , rotationTime * smooth * Time.deltaTime); 

		AudioSource audio = GetComponent<AudioSource>();

		if(transform.eulerAngles.y < 180)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Clamp (transform.eulerAngles.y, 0, maximumRotation), transform.eulerAngles.z);

			if(transform.eulerAngles.y >= maximumRotation)
				{
					canTurnClockwise = false;
			
						//lockSoundPlay = false;
						if(lockSoundPlay)
						{
							PlayLockSound();
							//lockSoundPlay = true;
						}
				}			
		else
		{
			canTurnClockwise = true;

			lockSoundPlay = true;

		}

		if(transform.eulerAngles.y == minimumRotation)
			{
				canTurnCounterClockwise = false;
				
				//lockSoundPlay = false;
				if(lockSoundPlay)
				{
					PlayLockSound();
					//lockSoundPlay = true;
				}

				
			}
			else
			{
				canTurnCounterClockwise = true;

				canTurnClockwise = true;
			}



			Debug.Log("Maximum Rotation");
		}
		else
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Clamp (transform.eulerAngles.y-360, minimumRotation, 0), transform.eulerAngles.z);

			

		}





	}


	public void PlayValveSound()
	{
		int index = Random.Range(0, valveSounds.Length);
         valveSoundClip = valveSounds[index];
         audioSource.clip = valveSoundClip;
         audioSource.Play();
	}

	public void PlayLockSound()
	{
		AudioSource audio = GetComponent<AudioSource>();	
		//if(!lockSoundPlay)
		//{


			//audio.clip = lockSound;
      		audio.PlayOneShot(lockSound, 1f);
			lockSoundPlay = false;
     	   Debug.Log("Locked");
   		// }
	}


	void rotateClockwise()
	{

			//transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
			

			 //targetRotation *=  Quaternion.AngleAxis(60, Vector3.up);

			//transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation , 10 * smooth * Time.deltaTime); 


			Debug.Log("Rotating Clockwise");
	}
}
