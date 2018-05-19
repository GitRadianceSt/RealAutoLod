using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour {

	public string KeyID;

public AudioClip PickupSound;
public GameObject DoorUnlock;
public bool  PlaySound;

void  Picked (){
	DoorController Door = DoorUnlock.GetComponent<DoorController>();
	Door.isLocked = false;
	if(PlaySound){
		GetComponent<AudioSource>().clip = PickupSound;
		GetComponent<AudioSource>().Play();
	}
	this.GetComponent<Collider>().enabled = false;
	this.GetComponent<Renderer>().enabled = false;
}
	void UseObject()
	{
		Picked ();
		//Remove ();
	
	}

	void Remove () {
		Destroy(gameObject);
	}

}