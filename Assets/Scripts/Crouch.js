#pragma strict

var standing : GameObject;
var crouching : GameObject;;
var crouchHead : GameObject;
var crouchCollider : CapsuleCollider;
var player : CharacterController;
var standHeight : float = 2.5;
var crouchHeight : float = 1.5;
var smooth : float = 0;

var LeftTexture : GameObject;
var RightTexture : GameObject;
 
function Update () {
    if (Input.GetButtonDown ("Crouch"))
        if( LeftTexture.activeSelf){
            RightTexture.SetActive (true);
            LeftTexture.SetActive (false);
        }
        else if (RightTexture.activeSelf){
            LeftTexture.SetActive (true);
            RightTexture.SetActive (false);
        }
    
 
    if (LeftTexture.activeSelf)
    {
        player.height = crouchHeight;
        crouchHead.SetActive (true);
        crouchCollider.enabled = true;
    }
 
    if (RightTexture.activeSelf)
    {
        player.height = player.height = Mathf.Lerp(player.height,standHeight,Time.deltaTime*smooth);
        crouchHead.SetActive (false);
        crouchCollider.enabled = false;
    }
}