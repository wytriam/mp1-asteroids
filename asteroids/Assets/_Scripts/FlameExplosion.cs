using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameExplosion : MonoBehaviour
{
    public float flameSpeed = 5;
    private Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        OnEnable();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnEnable()
    {
        if (rb != null)
            rb.velocity = -1 * transform.up * flameSpeed;
    }

    void OnDisable()
    {
        if (rb != null)
            rb.velocity = Vector3.zero;
    }
}
