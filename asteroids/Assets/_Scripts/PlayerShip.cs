using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public float thrust = 5f;
    //public float maxSpeed = 10f;
    public float rotSpeed = 5f;

    Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Apply force to ship based on input
        rb.AddForce(rb.transform.up * thrust * Input.GetAxis("Vertical"), ForceMode.Acceleration);

        // Rotate ship with A/D or Left Arrow/Right Arrow
        rb.rotation *= Quaternion.Euler(0, 0, Input.GetAxis("Horizontal") * -rotSpeed);
    }
}
