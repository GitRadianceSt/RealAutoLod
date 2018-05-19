using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {


	//[TextArea]
	public string DoorID;
	public GameObject keyObject;

	public bool Pressed = false;

	public bool isOpen = false;


	//public GUICrosshair Reticle;
	public bool  isLocked = false;
	public bool  isJammed = false;
	public bool  isUnlocked = false;

	private bool  guiShow = false;

	public AudioSource audioSource;

	/* DOOR TEXTS in CLASS */
	[System.Serializable]
	public class DoorTextClass 
	{
		public string DoorLockedText = "The Door is Locked find Key";
		public string DoorJammedText = "The Door is Jammed";
	}
	public DoorTextClass DoorText = new DoorTextClass ( ) ;

	/* DOOR SOUNDS in CLASS */
	[System.Serializable]
	public class DoorSoundsClass { 
		public bool PlayDoorSound;
		public AudioClip DoorOpenSound;
		public AudioClip DoorCloseSound;

		public bool LockedJammedSound;
		public AudioClip DoorLocked;
		public AudioClip DoorJammed;
		public AudioClip UnlockingDoor;
	}

	public DoorSoundsClass DoorSounds = new DoorSoundsClass ( ) ;

		/* ANIMATIONS */
		public GameObject WholeLock;
		public GameObject BottomLock;
		public GameObject TopLock;

		public GameObject DoorLeft;
		public GameObject DoorRight;
		
		public GameObject Knob;



	void UseObject ()
	{
		if (isOpen == false)
		{

			//Debug.Log ("Used");
			StartCoroutine(Open ());
			if(isOpen == true) 
			{
				isOpen = true;
			}
		}
		if (isOpen == true) 
		{

			Close ();
			if(isOpen == false) 
			{
				isOpen = false;
			}
		}
	}


	IEnumerator  Open (){

		Debug.Log ("DoorOpen");
		Pressed = true;
		if(isLocked == true){
			if(isUnlocked == false){
				GetComponent<AudioSource>().clip = DoorSounds.DoorLocked;
				if (!audioSource.isPlaying) 
				{
					GetComponent<AudioSource> ().PlayOneShot(DoorSounds.DoorLocked);
				}
			}
			GetComponent<AudioSource>().clip = DoorSounds.DoorJammed;
			if (!audioSource.isPlaying) 
			{
				GetComponent<AudioSource> ().PlayOneShot(DoorSounds.DoorJammed);
			}
		}
		//if(isOpen == false && isLocked == false && isJammed == false && DoorID == keyObject.GetComponent<KeyPickup>().KeyID){
		if(isOpen == false && isLocked == false && isJammed == false)
		{
			if (isUnlocked == false) 
			{
				GetComponent<AudioSource>().clip = DoorSounds.UnlockingDoor;
				GetComponent<AudioSource>().Play();
				StartCoroutine(Unlock ());
				yield return new WaitForSeconds (4);

				DoorLeft.GetComponent<Animation>().Play("DoorLeftOpen");
				DoorRight.GetComponent<Animation>().Play("DoorRightOpen");
				if(DoorSounds.PlayDoorSound)
				{
					GetComponent<AudioSource>().clip = DoorSounds.DoorOpenSound;
					GetComponent<AudioSource>().Play();
				}
				isOpen = true;
			}
		}
	}

	void  Close (){
		Pressed = true;
		if(DoorSounds.LockedJammedSound == true){
			if(isLocked){
				GetComponent<AudioSource>().clip = DoorSounds.DoorLocked;
				GetComponent<AudioSource>().Play();
			}
			if(isJammed){
				GetComponent<AudioSource>().clip = DoorSounds.DoorJammed;
				GetComponent<AudioSource>().Play();
			}
		}
		if(isOpen == true && isLocked == false && isJammed == false){
			DoorLeft.GetComponent<Animation>().Play("DoorLeftClose");
			DoorRight.GetComponent<Animation>().Play("DoorRightClose");
			if(DoorSounds.PlayDoorSound){
				GetComponent<AudioSource>().clip = DoorSounds.DoorCloseSound;
				GetComponent<AudioSource>().Play();
			}		
			isOpen = false;
		}
	}

	IEnumerator WaitPressed (){
		if(Pressed == true){
			yield return new WaitForSeconds(2);
			Pressed = false;
		}
	}

	IEnumerator Unlock ()
	{
		isUnlocked = true;
		yield return new WaitForSeconds (2);
		TopLock.GetComponent<Animation>().Play("Lock 1");
		//yield return new WaitForSeconds (1);
		BottomLock.GetComponent<Animation>().Play("Lock 2");
		yield return new WaitForSeconds (1);
		WholeLock.GetComponent<Animation>().Play("Lock 3");
		yield return new WaitForSeconds (1);
		Knob.GetComponent<Animation>().Play("Knob");

		
	}

	void  OnGUI (){
		if(Pressed && isLocked == true && isJammed == false)
		{
			GUI.Label( new Rect(Screen.width /2 -62.5f, Screen.height /2 + 50, 200, 100), DoorText.DoorLockedText);
			WaitPressed();
		}

		if(Pressed && isJammed == true && isLocked == false)
		{
			GUI.Label( new Rect(Screen.width /2 -62.5f, Screen.height /2 + 50, 200, 100), DoorText.DoorJammedText);
			WaitPressed();
		}
	}
}