using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public float thrust = 5f;
    //public float maxSpeed = 10f;
    public float rotSpeed = 5f;

    [Header("Projectile Details")]
    public GameObject projectilePrefab;
    public float projectileSpeed;
    // these two variables are used to control where the projectile fires from. 
    public GameObject[] guns;
    private int gun = 0;

    Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");

        // Apply force to ship based on input
        rb.AddForce(rb.transform.up * thrust * vInput, ForceMode.Acceleration);
        rb.AddForce(rb.transform.right * thrust * hInput, ForceMode.Acceleration);

        // Rotate ship with A/D or Left Arrow/Right Arrow
        rb.rotation *= Quaternion.Euler(0, 0, hInput * -rotSpeed);
        // Allow the ship to fire
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TempFire();
        }
    }

    void TempFire()
    {
        // guns[gun] gets the current gun location from the guns prefab
        GameObject projGO = Instantiate<GameObject>(projectilePrefab, guns[gun].transform);
        // set up the next guns object: 
        gun = (gun + 1) % guns.Length;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        rigidB.velocity = transform.up * projectileSpeed;
    }
}
