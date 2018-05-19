﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class GroundBehaviour : MonoBehaviour 
{
	public List<GroundType> GroundTypes = new List<GroundType>();
	public UnityStandardAssets.Characters.FirstPerson.FirstPersonController controller;
	public CharacterController charcont;
	public string currentground;

	// Use this for initialization
	void Start () 
	{
		setGroundType(GroundTypes [0]);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnControllerColliderHit(ControllerColliderHit hit)  
	{
		if (hit.transform.tag == "MetalPlate")
			setGroundType (GroundTypes[1]);
		else if(hit.transform.tag == "Grass")
			setGroundType (GroundTypes[2]);
		else if(charcont.isGrounded)
			setGroundType (GroundTypes[0]);

	}

	public void setGroundType(GroundType ground)
	{
		if(currentground != ground.name)
		{
			//we need to change m_footstepsounds from private to public in controller first
			controller.m_FootstepSounds = ground.footstepsounds;
			//controller.m_WalkSpeed = ground.walkSpeed;
			//controller.m_RunSpeed = ground.runSpeed;
			currentground = ground.name;
		}
	}
}

[System.Serializable]
public class GroundType
{
	public string name;
	//you need at least 2 footstepsounds!!!!
	public AudioClip[] footstepsounds;
	//here you can also add for example the jump and land sound
	//public float walkSpeed = 2.5f;
	//public float runSpeed = 5;

}