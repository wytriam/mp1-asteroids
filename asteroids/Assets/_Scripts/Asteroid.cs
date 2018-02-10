using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Vector2 velocityRange = new Vector2(4, 6);
    public bool randomizeInitialSettings = true;
    public GameObject splitAsteroid;

    float speed;
    float rot;

    Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        if (randomizeInitialSettings)
        {
            speed = Random.Range(velocityRange.x, velocityRange.y);
            rot = Random.Range(0, 360);

            rb = GetComponent<Rigidbody>();

            transform.rotation = Quaternion.Euler(0, 0, rot);
            rb.velocity = rb.transform.up * speed;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Projectile")
        {
            Destroy(c.gameObject);
            Split();
        }
    }

    // splits an asteroid into smaller asteroids, and destroys the current asteroid
    public void Split()
    {
        if (splitAsteroid == null)
        {
            Destroy(gameObject);
            return;
        }

        // create sub asteroids
        for (int i = 0; i < 2; i++)
        {
            // create new asteroid. NOTE - CANNOT USE THE PARENT TRANSFORM
            GameObject asteroidGO = Instantiate<GameObject>(splitAsteroid, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Quaternion());
            // turn off randomization of initial settings
            asteroidGO.GetComponent<Asteroid>().randomizeInitialSettings = false;
            // set new asteroid rotation (old + randomized, within +-30 degrees)
            asteroidGO.transform.rotation = Quaternion.Euler(0, 0, gameObject.transform.rotation.eulerAngles.z + Random.Range(-30, 30));
            // set new asteroid velocity
            asteroidGO.GetComponent<Rigidbody>().velocity = asteroidGO.transform.up * (speed + Random.Range(-1, 1));
        }

        // destroy this asteroid
        Destroy(gameObject);
    }
}
