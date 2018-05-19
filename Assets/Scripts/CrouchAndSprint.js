#pragma strict

var standing :GameObject;
var crouching :GameObject;
var disable : GameObject;
var player : CharacterController;
var standHeight : float = 0;
var smooth : float = 0;
var proneBlocked : boolean;
var crouch : Crouch;

var LeftTexture : GameObject;
var RightTexture : GameObject;

function Update () {
    if (Input.GetButton("Sprint") && Input.GetAxis("Horizontal") != 0 || Input.GetButton("Sprint") && Input.GetAxis("Vertical") != 0 && disable.activeSelf || Input.GetButton("Jump") && disable.activeSelf)
        if(proneBlocked == false)
    {
        LeftTexture.SetActive (false);
        RightTexture.SetActive (true);
        player.height = player.height = Mathf.Lerp(player.height,standHeight,Time.deltaTime*smooth);
        crouch.enabled = false;
        }
   
    
    if (Input.GetButtonUp("Sprint"))
        {
        crouch.enabled = true;
        }

    if(proneBlocked)
    {
        LeftTexture.SetActive (true);
        RightTexture.SetActive (false);
    }
}

function OnTriggerEnter (other : Collider){
    if (other.gameObject.tag == "Crouch"){
        proneBlocked = true;
    }
}

    //Deactivate the Main function when player is go away from door
    function OnTriggerExit (other : Collider){
        if (other.gameObject.tag == "Crouch"){
            proneBlocked = false;
        }
    }
