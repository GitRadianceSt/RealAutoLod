
var obj : GameObject;

function OnTriggerEnter (col : Collider){
if(col.gameObject.tag == "Player"){
obj.SetActive(true);
}
}
function OnTriggerExit (col : Collider){


obj.SetActive(false);

}