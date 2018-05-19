using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerAnimations : MonoBehaviour {


	public Animator animator;
	public GameObject playerController;
	public CharacterController controller;
	bool isReadyToRun = false;
	public GameObject sprinter;

	// Use this for initialization
	void Start () 
	{
		animator = GameObject.Find("WeaponHolder").GetComponent<Animator>();
		//animator.SetBool("isIdle", true);
	}
	
	// Update is called once per frame
	void Update () {


		playerController = GameObject.FindWithTag ("Player");
		controller = GetComponent<CharacterController>();
		//sprinter = FindWithTag("Player").GetComponent<Sprinter> ();

		//GameObject.FindWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>()

		//if(GameObject.FindWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_IsWalking = true)
		//if(playerController.GetComponent<Rigidbody>().velocity.magnitude > 0)

		//if(controller.velocity.magnitude > 0 && Input.GetKey(KeyCode.LeftShift)  && controller.isGrounded)
		if(controller.velocity.magnitude > 0 && sprinter.GetComponent<Sprinter>().isRunning  && controller.isGrounded)

		{
			animator.SetBool("isRunning", true);
			animator.SetBool("isIdle", false);
			animator.SetBool("isWalking", false);
		}



		else if(controller.velocity.magnitude > 0 && controller.isGrounded)

		{
			animator.SetBool("isWalking", true);
			animator.SetBool("isIdle", false);
			animator.SetBool("isRunning", false);
			//isReadyToRun = true;
		}
			
	
		else
		{
			animator.SetBool("isIdle", true);
			animator.SetBool("isRunning", false);
			animator.SetBool("isWalking", false);
		}

	}
}
