#pragma strict

var animator : Animator;
var anim1 : boolean;
var anim2 : boolean;
var anim3 : boolean;



function Start () {
    animator.enabled = false;
    anim1 = false;
    anim2 = false;
    anim2 = false;
}



function Update () {


    if (Input.GetKeyDown(KeyCode.E))    //ANIM1
    {
        animator.enabled = true;
        anim1 = true;
        anim2 = false;
        anim3 = false;
    }

    if (Input.GetKeyDown(KeyCode.R)){   //ANIM2  
        animator.enabled = true;
        anim2 = true;
        anim1 = false;
        anim3 = false;
    }

    if (Input.GetKeyDown(KeyCode.T)){   //ANIM3
        animator.enabled = true;
        anim3 = true;
        anim1 = false;
        anim2 = false;
    }

    if(anim1 == true) {
        animator.SetBool("anim1", true);
        animator.SetBool("anim2", false);
        animator.SetBool("anim3", false);
    }

    if(anim2 == true) {
        animator.SetBool("anim2", true);
        animator.SetBool("anim1", false);
        animator.SetBool("anim3", false);
    }

    if(anim3 == true) {
        animator.SetBool("anim3", true);
        animator.SetBool("anim2", false);
        animator.SetBool("anim1", false);
    }
}




