using UnityEngine;

public class Rotator : MonoBehaviour
{
	public float speed = 25f;
	void Update () 
	{
		transform.Rotate (Vector3.up * speed * Time.deltaTime);
	}
}
