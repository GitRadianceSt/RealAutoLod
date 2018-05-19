/*
GUINotes.cs - wirted by ThunderWire Games * Script for Interact Notes
*/

using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class GUINotes : MonoBehaviour {

public float PickupRange = 3f;

private bool pressedButton = false;
private bool backNotes = false;

private Ray playerAim;
private Camera playerCam;
private GameObject NoteObject;

	void FixedUpdate () {
		//if(Input.GetButtonDown("Use") && !pressedButton){
		if(Input.GetButtonDown("Use")){
			//pressedButton = true;
			//Debug.Log ("Use");
		}
	}
	
	void Update () {
		playerCam = Camera.main;
		Ray playerAim = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		RaycastHit hit;

		if (Physics.Raycast (playerAim, out hit, PickupRange)){
			//Debug.Log ("readyToBePressed");
			if (hit.collider.tag == "InteractNote"){
				NoteObject = hit.collider.gameObject;
				//if(pressedButton){
				if(Input.GetButtonDown("Use")){
					NoteObject.GetComponent<Notes>().ShowNotes();
					playerCam.GetComponent<Blur>().enabled = true;
					backNotes = false;
					pressedButton = false;
					Debug.Log ("pressedButton");
				}
			}
		}
		if(backNotes){
			NoteObject.GetComponent<Notes>().BackNotes();
			playerCam.GetComponent<Blur>().enabled = false;
			backNotes = false;
			pressedButton = false;
		}
    }
	
	public void Back() {
		backNotes = true;
	}
}