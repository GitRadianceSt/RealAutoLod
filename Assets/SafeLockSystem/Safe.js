
public var input : String;
public var code : String = "1212";
var object : GameObject;
public var maxString = 4 ;
//var Text : UnityEngine.UI.Text;
var button1 : GameObject;
var button2 : GameObject;
var button3 : GameObject;
var button4 : GameObject;
var button5 : GameObject;
var button6 : GameObject;
var button7 : GameObject;
var button8 : GameObject;
var button9 : GameObject;
var button0 : GameObject;
var handle : Transform;
var handleTarget: Transform;
var door : Transform;
var doorTarget : Transform;
var beep1 : AudioClip;
var beep2 : AudioClip;


function Update () {
if(Input.GetKeyDown("1")){


input = input + "1";
GetComponent.<AudioSource>().PlayOneShot(beep1);
button1.GetComponent.<Renderer>().material.color = Color.red;
de1();
}
if(Input.GetKeyDown("3")){


input = input + "3";
GetComponent.<AudioSource>().PlayOneShot(beep1);
button3.GetComponent.<Renderer>().material.color = Color.red;
de3();

}
if(Input.GetKeyDown("4")){


input = input + "4";
GetComponent.<AudioSource>().PlayOneShot(beep1);
button4.GetComponent.<Renderer>().material.color = Color.red;
de4();

}
if(Input.GetKeyDown("5")){


input = input + "5";
GetComponent.<AudioSource>().PlayOneShot(beep1);
button5.GetComponent.<Renderer>().material.color = Color.red;
de5();

}
if(Input.GetKeyDown("6")){


input = input + "6";
GetComponent.<AudioSource>().PlayOneShot(beep1);
button6.GetComponent.<Renderer>().material.color = Color.red;
de6();

}
if(Input.GetKeyDown("7")){


input = input + "7";
GetComponent.<AudioSource>().PlayOneShot(beep1);
button7.GetComponent.<Renderer>().material.color = Color.red;
de7();

}
if(Input.GetKeyDown("8")){


input = input + "8";
GetComponent.<AudioSource>().PlayOneShot(beep1);
button8.GetComponent.<Renderer>().material.color = Color.red;
de8();

}
if(Input.GetKeyDown("9")){


input = input + "9";
GetComponent.<AudioSource>().PlayOneShot(beep1);
button9.GetComponent.<Renderer>().material.color = Color.red;
de9();

}
if(Input.GetKeyDown("0")){


input = input + "0";
GetComponent.<AudioSource>().PlayOneShot(beep1);
button0.GetComponent.<Renderer>().material.color = Color.red;
de0();

}
if(Input.GetKeyDown("2")){

input = input + "2";
GetComponent.<AudioSource>().PlayOneShot(beep1);
button2.GetComponent.<Renderer>().material.color = Color.red;
de2();

}
}
function FixedUpdate(){

if(input == code){


object.SetActive(true);
//Text.color = Color.green;
open();

}
if(input.Length > maxString){

GetComponent.<AudioSource>().Stop();
GetComponent.<AudioSource>().PlayOneShot(beep2);

input = "";

}
//Text.text = input;
}
function de1(){

yield WaitForSeconds(0.1);
button1.GetComponent.<Renderer>().material.color = Color.white;

}
function de2(){

yield WaitForSeconds(0.1);
button2.GetComponent.<Renderer>().material.color = Color.white;

}
function de3(){

yield WaitForSeconds(0.1);
button3.GetComponent.<Renderer>().material.color = Color.white;

}
function de4(){

yield WaitForSeconds(0.1);
button4.GetComponent.<Renderer>().material.color = Color.white;

}
function de5(){

yield WaitForSeconds(0.1);
button5.GetComponent.<Renderer>().material.color = Color.white;

}
function de6(){

yield WaitForSeconds(0.1);
button6.GetComponent.<Renderer>().material.color = Color.white;

}
function de7(){

yield WaitForSeconds(0.1);
button7.GetComponent.<Renderer>().material.color = Color.white;

}
function de8(){

yield WaitForSeconds(0.1);
button8.GetComponent.<Renderer>().material.color = Color.white;

}
function de9(){

yield WaitForSeconds(0.1);
button9.GetComponent.<Renderer>().material.color = Color.white;

}
function de0(){

yield WaitForSeconds(0.1);
button0.GetComponent.<Renderer>().material.color = Color.white;

}
function open(){

handle.transform.rotation.z = handleTarget.transform.rotation.z;

doorOpen();

}
function doorOpen(){

yield WaitForSeconds(1);
door.transform.rotation.y = doorTarget.transform.rotation.y;
//Destroy(Text);

}



