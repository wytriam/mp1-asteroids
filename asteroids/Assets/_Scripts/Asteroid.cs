using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Vector2 velocityRange = new Vector2(4, 6);
    public GameObject splitAsteroid;

    float speed;
    float rot;

    Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        speed = Random.Range(velocityRange.x, velocityRange.y);
        rot = Random.Range(0, 360);

        rb = GetComponent<Rigidbody>();

        transform.rotation = Quaternion.Euler(0, 0, rot);
        rb.velocity = rb.transform.up * speed;
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    // splits an asteroid into smaller asteroids, and destroys the current asteroid
    public void Split()
    {

    }
}
