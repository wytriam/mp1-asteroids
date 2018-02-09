using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    float speed;
    float rot;

    Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        speed = Random.Range(4, 6);
        rot = Random.Range(0, 360);

        rb = GetComponent<Rigidbody>();

        transform.rotation = Quaternion.Euler(0, 0, rot);
        rb.velocity = rb.transform.up * speed;
	}
	
	// Update is called once per frame
	void Update ()
    {

    }
}
