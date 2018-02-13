using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public bool testMode = false;
    public Vector2 velocityRange = new Vector2(4, 6);
    public bool randomizeInitialSettings = true;
    public GameObject splitAsteroid;

    float speed;
    float rot;

    Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        // not in test mode, so let game manager know you exist
        if (!testMode)
        {
            Messenger.Broadcast(Messages.INST_ASTEROID);
        }
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
        if (c.gameObject.tag == "Player")
        {
            Destroy(c.gameObject);
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
            // create new asteroid. NOTE - CANNOT USE THE PARENT TRANSFORM. THAT WOULD MAKE THE NEW OBJECT A CHILD OF THE CURRENT OBJECT, AND WOULD LEAD TO IT BEING DESTROYED AT THE END OF THIS FUNCTION
            GameObject asteroidGO = Instantiate<GameObject>(splitAsteroid, new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y + Random.Range(-10, 10), transform.position.z), new Quaternion());
            // turn off randomization of initial settings
            asteroidGO.GetComponent<Asteroid>().randomizeInitialSettings = false;
            // set new asteroid rotation (old + randomized, within +-30 degrees)
            asteroidGO.transform.rotation = Quaternion.Euler(0, 0, gameObject.transform.rotation.eulerAngles.z + Random.Range(-30, 30));
            // set new asteroid velocity
            asteroidGO.GetComponent<Rigidbody>().velocity = asteroidGO.transform.up * (speed + Random.Range(2, 4));
        }

        // destroy this asteroid
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        Messenger.Broadcast(Messages.DEST_ASTEROID);
    }
}
