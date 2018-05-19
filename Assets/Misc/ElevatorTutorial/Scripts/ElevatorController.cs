/*
ElevatorController.cs - wirted by ThunderWire Games * Script for Simple Elevator Moving
*/

using UnityEngine;
using System.Collections;
	
	[System.Serializable]
	public class m_ElevatorSounds {
		public AudioClip ElevatorStartMoving;
		public AudioClip ElevatorStop;
	}

	[RequireComponent (typeof (AudioSource))]
public class ElevatorController : MonoBehaviour {
	
	[Tooltip("Elevator move speed.")]
	public float elevatorSpeed = 0.05f;
	
	[Tooltip("Elevator floors.")]
	public Transform[] ElevatorFloors;
	
	public m_ElevatorSounds ElevatorSounds =  new m_ElevatorSounds();	
	
	[Tooltip("Set elevator position to floor.")]
	public int CurrentFloor = 0;
	
	private int floorNumber = 0;
	private string elevatorDirection;
	[HideInInspector]
	public bool ElevatorMoving;
	private bool ElevatorMax;
	private bool ElevatorMin;
	private bool soundplayed;
	private bool m_soundplayed;
	private bool isMoved = false;
	
	void Start ()
	{
		transform.position = ElevatorFloors[CurrentFloor].position;
		this.GetComponent<AudioSource>().Stop();
	}
	
	public void ElevatorGO (string ElevatorDirection) {
		elevatorDirection = ElevatorDirection;
		FloorNumber();
		soundplayed = false;
		m_soundplayed = false;
	}
	
	void FloorNumber ()
	{
		floorNumber = Mathf.Clamp(floorNumber, 0, ElevatorFloors.Length - 1);
		if(elevatorDirection == "ElevatorUp" && !ElevatorMoving && !ElevatorMax)
		{
			floorNumber += 1;
		}
		if(elevatorDirection == "ElevatorDown" && !ElevatorMoving && !ElevatorMin)
		{
			floorNumber -= 1;
		}		
	}
	
	void FixedUpdate() {
		Transform ElevatorFloor = ElevatorFloors[floorNumber];
		if(elevatorDirection == "ElevatorUp")
		{
			ElevatorMoving = true;
			transform.position = Vector3.MoveTowards(transform.position, ElevatorFloor.position, elevatorSpeed);
		}
		
		if(elevatorDirection == "ElevatorDown")
		{
			ElevatorMoving = true;
			transform.position = Vector3.MoveTowards(transform.position, ElevatorFloor.position, elevatorSpeed);
		}
		
		if(this.transform.position == ElevatorFloor.position)
		{
			ElevatorMoving = false;
		}
		
		if(this.transform.position == ElevatorFloors[0].position)
		{
			ElevatorMax = false;
			ElevatorMin = true;
		}
		
		if(this.transform.position.y > ElevatorFloors[0].position.y)
		{
			ElevatorMin = false;
		}
		
		int MaxFloors = ElevatorFloors.Length - 1;
		if(this.transform.position == ElevatorFloors[MaxFloors].position)
		{
			ElevatorMax = true;
			ElevatorMin = false;
		}
		
		if(this.transform.position.y < ElevatorFloors[MaxFloors].position.y)
		{
			ElevatorMax = false;
		}
		
		if(ElevatorMoving && !isMoved)
		{
			if(!m_soundplayed){
				this.GetComponent<AudioSource>().clip = ElevatorSounds.ElevatorStartMoving;
				this.GetComponent<AudioSource>().Play();
				m_soundplayed = true;
			}
			isMoved = true;
		}
		if(this.transform.position == ElevatorFloors[floorNumber].position && !ElevatorMoving && isMoved)
		{
			if(!soundplayed){
				this.GetComponent<AudioSource>().Stop();
				this.GetComponent<AudioSource>().PlayOneShot(ElevatorSounds.ElevatorStop);
				soundplayed = true;
			}
			isMoved = false;
		}
	}
	
	void OnDrawGizmos() {
		for(int i = 0; i < ElevatorFloors.Length; i++) {
			Gizmos.color = Color.red;
			float x = ElevatorFloors[i].position.x;
			float y = ElevatorFloors[i].position.y;
			float z = ElevatorFloors[i].position.z;
			Gizmos.DrawWireCube(new Vector3(x, y+1.502f, z), new Vector3(2.4f, 3.0f, 2.2f));
		}
	}
}
