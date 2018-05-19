#pragma strict

var Pic : GameObject;
var sound : AudioClip;
var text : GameObject;


function Start () {
	Pic.SetActive(false);
	text.SetActive (false);
}

function Update () {
if(Input.GetKeyDown(KeyCode.Mouse1)) { 
Pic.SetActive(false);
text.SetActive (false);

}
}



 

function OnTriggerStay (col : Collider) {

 
        if(col.gameObject.tag == "attackArea") {
        		 if(Input.GetKeyDown(KeyCode.Mouse0)) {
 					text.SetActive (true);
 					Pic.SetActive(true);
 					
 					AudioSource.PlayClipAtPoint(sound, transform.position);
 					}	
 				
						
                    
        		}
		}
				         	



