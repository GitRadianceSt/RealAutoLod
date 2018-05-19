using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class Crouch : MonoBehaviour {

    //=================================================================================
    // © 2016 Scott Durkin, All rights reserved.
    // By Downloading and using this script credit must 
    // be given to the creator know as "Unity3D With Scott".
    // YouTube Channel: https://www.youtube.com/channel/UC9hfBvn17qSIrdFwAk56oZg
    //=================================================================================

    public CharacterController characterController;
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController controller;
   
   	public float crouchWalkSpeed;
    //public float crouchRunSpeed;
	
	public float defWalkSpeed;
    public float defRunSpeed;

    public float defJumpSpeed;


    void Start ()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    void Update ()
    {
        if (Input.GetButton("Crouch"))
        {
            characterController.height = 1f;
            controller.m_WalkSpeed = crouchWalkSpeed;
			       controller.m_RunSpeed = crouchWalkSpeed;
			      controller.m_JumpSpeed = 0f;
			       Debug.Log("Crouched");
        }
       else
      
       // {
         //   characterController.height = 1.8f;
           // controller.m_WalkSpeed = defWalkSpeed;
		//	controller.m_RunSpeed = defRunSpeed;
        //}
    
     //if (Input.GetButtonUp("Crouch"))
     //if (Input.GetButtonUp("Crouch") && characterController.height > 1.79f)

     {

              Ray ray = new Ray();
              RaycastHit hit;
              ray.origin = transform.position;
              ray.direction = Vector3.up;
              if(!Physics.Raycast(ray, out hit, 1))
              {
                  //t_mesh.localScale = new Vector3(1, 1, 1);
                  characterController.height = 1.8f;
                  controller.m_WalkSpeed = defWalkSpeed;
				          controller.m_RunSpeed = defRunSpeed;
				           controller.m_JumpSpeed = defJumpSpeed;
                  Debug.Log("Uncrouched");
              } 
              else 
              {
                 characterController.height = 1f;
                

                 Debug.Log("Not enough space to stand up!");

                 if (Input.GetButtonUp("Crouch") && characterController.height > 1.79f)
                 {
                    controller.m_WalkSpeed = defWalkSpeed;
                    controller.m_RunSpeed = defRunSpeed;
                    controller.m_JumpSpeed = defJumpSpeed;
                    
                    Debug.Log("Should now stand up!");
                 }

              }
         }
    }
}
