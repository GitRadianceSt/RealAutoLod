using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveController : MonoBehaviour {

	public float rotationAmount = 30f;
	private float nextTime = 0;
	public float useRate = 0.25f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButtonDown("RotateClockwise") && Time.time > nextTime)
		{

			nextTime = Time.time + useRate;
			RaycastHit hit;
      	  Ray ray = new Ray(transform.position, transform.forward);
		
			if (Physics.Raycast(ray, out hit))
				{
 		 			if (hit.collider.tag == "Valve") 
 		 			{
 		 				if(hit.collider.gameObject.GetComponent<ValveRotator2>().canTurnClockwise == true)
 		 				{
  				 		      //hit.collider.gameObject.GetComponent<ValveRotator>().RotateClockwise();
 		 					//hit.collider.gameObject.GetComponent<ValveRotator2>().targetRotation *=  Quaternion.AngleAxis(rotationAmount, Vector3.up);
 		 					hit.collider.gameObject.GetComponent<ValveRotator2>().y += rotationAmount;
 		 					hit.collider.gameObject.GetComponent<ValveRotator2>().PlayValveSound();
  		  		 		}
					}
				}
		}

		if(Input.GetButtonDown("RotateCounterClockwise") && Time.time > nextTime)
		{

			nextTime = Time.time + useRate;
			RaycastHit hit;
      	  Ray ray = new Ray(transform.position, transform.forward);
		
			if (Physics.Raycast(ray, out hit))
				{
 		 			if (hit.collider.tag == "Valve") 
 		 			{
 		 				if(hit.collider.gameObject.GetComponent<ValveRotator2>().canTurnCounterClockwise == true)
 		 				{
  				 	 	     //hit.collider.gameObject.GetComponent<ValveRotator>().RotateClockwise();
 		 					//it.collider.gameObject.GetComponent<ValveRotator2>().targetRotation *=  Quaternion.AngleAxis(-rotationAmount, Vector3.up);
 		 					hit.collider.gameObject.GetComponent<ValveRotator2>().y -= rotationAmount;
 		 					hit.collider.gameObject.GetComponent<ValveRotator2>().PlayValveSound();
  		  		 		}
					}
				}
		}

	}
}
