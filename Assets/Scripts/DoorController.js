/*
DoorController.js - wirted by ThunderWire Games * Script for Door Open/Close and Locked/Jammed
*/

#pragma strict

@HideInInspector
public var Pressed : boolean = false;
@HideInInspector
var isOpen : boolean = false;


var Reticle : GameObject;
var isLocked : boolean = false;
var isJammed : boolean = false;

private var guiShow : boolean = false;

/* DOOR TEXTS in CLASS */
class DoorTextClass {
var DoorLockedText : String = "The Door is Locked find Key";
var DoorJammedText : String = "The Door is Jammed";
}
var DoorText : DoorTextClass = new DoorTextClass ( ) ;

/* DOOR SOUNDS in CLASS */
class DoorSoundsClass { 
var PlayDoorSound : boolean;
var DoorOpenSound : AudioClip;
var DoorCloseSound : AudioClip;

var LockedJammedSound : boolean;
var DoorLocked : AudioClip;
var DoorJammed : AudioClip;
}
var DoorSounds : DoorSoundsClass = new DoorSoundsClass ( ) ;

	function Open() {
		Pressed = true;
		if(DoorSounds.LockedJammedSound == true){
			if(isLocked){
				GetComponent.<AudioSource>().clip = DoorSounds.DoorLocked;
				GetComponent.<AudioSource>().Play();
			}
			if(isJammed){
				GetComponent.<AudioSource>().clip = DoorSounds.DoorJammed;
				GetComponent.<AudioSource>().Play();
			}
		}
		if(isOpen == false && isLocked == false && isJammed == false){
			GetComponent.<Animation>().Play("DoorOpen");
			if(DoorSounds.PlayDoorSound){
				GetComponent.<AudioSource>().clip = DoorSounds.DoorOpenSound;
				GetComponent.<AudioSource>().Play();
			}
			isOpen = true;
		}
	}
	
	function Close(){
		Pressed = true;
		if(DoorSounds.LockedJammedSound == true){
			if(isLocked){
				GetComponent.<AudioSource>().clip = DoorSounds.DoorLocked;
				GetComponent.<AudioSource>().Play();
			}
			if(isJammed){
				GetComponent.<AudioSource>().clip = DoorSounds.DoorJammed;
				GetComponent.<AudioSource>().Play();
			}
		}
		if(isOpen == true && isLocked == false && isJammed == false){
			GetComponent.<Animation>().Play("DoorClose");
			if(DoorSounds.PlayDoorSound){
				GetComponent.<AudioSource>().clip = DoorSounds.DoorCloseSound;
				GetComponent.<AudioSource>().Play();
			}		
			isOpen = false;
		}
	}
	
	function WaitPressed() {
		if(Pressed == true){
			yield WaitForSeconds(2);
			Pressed = false;
		}
	}
	

	function OnGUI(){
		if(Pressed && isLocked == true && isJammed == false)
		{
			GUI.Label(Rect(Screen.width /2 -62.5, Screen.height /2 + 50, 200, 100), DoorText.DoorLockedText);
			WaitPressed();
		}
	
		if(Pressed && isJammed == true && isLocked == false)
		{
			GUI.Label(Rect(Screen.width /2 -62.5, Screen.height /2 + 50, 200, 100), DoorText.DoorJammedText);
			WaitPressed();
		}
	}