using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveRotator2 : MonoBehaviour {

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

	 public float x = 0.0f;
     public float y = 0.0f;
     public float z = 0.0f;

	// Use this for initialization
	void Start () 
	{
		targetRotation = transform.rotation;
		//lockSoundPlay = false;

		 x = transform.localEulerAngles.x;
         y = transform.localEulerAngles.y;
         z = transform.localEulerAngles.z;

	}
	
	// Update is called once per frame
	void Update () 
	{
		//transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation , rotationTime * smooth * Time.deltaTime); 

		y = ClampAngle( y, minimumRotation, maximumRotation );

		Quaternion newRot = Quaternion.Euler( x, y, z );

		transform.localRotation = Quaternion.Lerp (transform.localRotation, newRot , rotationTime * smooth * Time.deltaTime); 

		//transform.rotation = newRot;


		AudioSource audio = GetComponent<AudioSource>();

				// rotationY = Mathf.Clamp(rotationY, -45.0f, 45.0f);
 
                 //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, -rotationY, transform.localEulerAngles.z);






			if(y == maximumRotation)
				{
					canTurnClockwise = false;
			
					if(lockSoundPlay == true)
					{
						PlayLockSound();
						lockSoundPlay = false;
					}
				}			
			else
			{
				canTurnClockwise = true;

				

			}

			if(y == minimumRotation)
				{
					canTurnCounterClockwise = false;
				
					if(lockSoundPlay == true)
					{
						PlayLockSound();
						lockSoundPlay = false;
					}

			}
			else
			{
				canTurnCounterClockwise = true;

				
			}


	}


	float ClampAngle( float angle, float min, float max )
     {
         if ( angle < -360 )
             angle += 360;
         if ( angle > 360 )
             angle -= 360;
         
         return Mathf.Clamp( angle, min, max );
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
      		
		//if(y == minimumRotation || y == maximumRotation)
		if(canTurnCounterClockwise == false || canTurnClockwise == false)
		{
      		//lockSoundPlay = true;
      		//if(lockSoundPlay)
      		//{
      		audio.PlayOneShot(lockSound, 1f);
			//lockSoundPlay = false;
     	   	Debug.Log("Locked");
     	   //}
   		}
	}


	void rotateClockwise()
	{

			//transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
			

			 //targetRotation *=  Quaternion.AngleAxis(60, Vector3.up);

			//transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation , 10 * smooth * Time.deltaTime); 


			Debug.Log("Rotating Clockwise");
	}
}
