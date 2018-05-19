using UnityEngine;
using System.Collections;

public class SimpleRotation : MonoBehaviour
{
    public float speed;
	void Update ()
    {
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, speed * Time.time);
	}
}
