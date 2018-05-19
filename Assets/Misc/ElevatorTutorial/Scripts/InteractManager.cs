/*
InteractManager.cs - wirted by ThunderWire Games * Script for Interact Buttons, Items etc.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractManager : MonoBehaviour {
	
	public float PickupRange = 3; 
	public string UseButton = "Use";
	
	private GameObject playerCam;
	private Ray playerAim;
	
	private bool isPressed;
	private GameObject objectInteract;
	
	void Start () {
		playerCam = Camera.main.gameObject;
		isPressed = false;
		objectInteract = null;
	}
	
	void Update () {
		if(Input.GetButton(UseButton) && !isPressed){
			Interact();
			isPressed = true;
		}
		if(Input.GetButtonUp(UseButton) && isPressed){
			isPressed = false;
		}
	}
	
	private void Interact(){
		Ray playerAim = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		RaycastHit hit;
		
		if (Physics.Raycast (playerAim, out hit, PickupRange)){
			objectInteract = hit.collider.gameObject;
			if(hit.collider.tag == "ElevatorUp"){
				InteractUse("ElevatorUp");
			}
			if(hit.collider.tag == "ElevatorDown"){
				InteractUse("ElevatorDown");
			}
		}
	}
	
    private void InteractUse(string Call)
    {
		objectInteract.GetComponent<ElevatorButton>().SendCall(Call);
    }
}
