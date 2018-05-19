using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cripe : MonoBehaviour {

	public Transform player;
	static Animator anime;
	// Use this for initialization
	void Start ()
	{
		anime = GetComponent<Animator>(); 
	}

	// Update is called once per frame
	void Update () {
		Vector3 direction = player.position - this.transform.position;
		float angle = Vector3.Angle(direction, this.transform.forward);
		if (Vector3.Distance(player.position, this.transform.position) < 17 && angle < 50)
		{
			direction.y = 0;

			this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
				Quaternion.LookRotation(direction), 0.1f);

			if(direction.magnitude > 2f)
			{
				anime.SetBool("isRunning", true);
				anime.SetBool("isIdle", false);
				anime.SetBool("isWalking", false);
				anime.SetBool("isAttacking", false);
				this.transform.Translate(0, 0, 0.08f);
			}
			//else if (direction.magnitude < 4 && direction.magnitude > 2f)
			//else if (direction.magnitude > 2f)
			//{
			//	anime.SetBool("isRunning", false);
			//	anime.SetBool("isIdle", false);
			//	anime.SetBool("isWalking", true);
			//	anime.SetBool("isAttacking", false);
			//	this.transform.Translate(0, 0, 0.05f);
			//}
			else if(direction.magnitude <= 2f)
			{
				anime.SetBool("isIdle", false);
				anime.SetBool("isAttacking", true);
				anime.SetBool("isWalking", false);
				anime.SetBool("isRunning", false);               
			}
			else
			{
				anime.SetBool("isIdle", true);
				anime.SetBool("isWalking", false);
				anime.SetBool("isAttacking", false);
				anime.SetBool("isRunning", false);
			}

		}
		else
		{
			anime.SetBool("isIdle", true);
			anime.SetBool("isWalking", false);
			anime.SetBool("isAttacking", false);
			anime.SetBool("isRunning", false);
		}
	}
}﻿