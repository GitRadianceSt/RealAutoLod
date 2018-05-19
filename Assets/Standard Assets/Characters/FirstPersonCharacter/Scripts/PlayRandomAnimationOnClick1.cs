﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomAnimationOnClick1 : MonoBehaviour {

	//public GameObject animObject;
	//public GameObject crowbar;
	public float crowbarStaminaHit = 0;
	Animator animator;
	//public bool increased = false;

	//public rand : int = Random.Range(1,2);

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		//increased = false;
	}

	// Update is called once per frame
	void Update()
	{

		if(Input.GetMouseButtonDown(0))
		{
			//int index = Random.Range(0, shoot.Length);
			//shootClip = shoot[index];
			//string animObject = string.Format("it {0}", Random.Range((int) 1, (int) 3));
			//randomNumber = Random.Range(1,2);

			//if (increased == false) {
			//	DecreaseStamina ();
			//}
			crowbarStaminaHit +=2;

			PlayAudio ();



			int rnd = Random.Range (0, 3);
			if(rnd == 1)

			{
				animator.SetBool("Hit 1", true);
				animator.SetBool("Hit 2", false);
				animator.SetBool("Hit 3", false);

			}
			else if(rnd == 2)
			{
					animator.SetBool("Hit 1", false);
					animator.SetBool("Hit 2", true);
					animator.SetBool("Hit 3", false);
			}
			else
			{
				animator.SetBool("Hit 1", false);
				animator.SetBool("Hit 2", false);
				animator.SetBool("Hit 3", true);
			}
				
		}
	
	}
	void PlayAudio()
	{
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();
		//yield return new WaitForSeconds (1.25f);

	}

	void AnimationEnded()

	{

		animator.SetBool("Hit 1", false);
		animator.SetBool("Hit 2", false);
		animator.SetBool("Hit 3", false);
	}

	//void DecreaseStamina()
	//{
	//	crowbarStaminaHit += 2;
	//	increased = true;
	//}

}