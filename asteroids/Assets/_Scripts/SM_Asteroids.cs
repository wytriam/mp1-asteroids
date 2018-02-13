using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Asteroids : WytriamSTD.Scene_Manager
{
    private Constants constants;
    private Spawner spawner;

	// Use this for initialization
	void Start ()
    {
        constants = GetComponent<Constants>();
        spawner = GetComponent<Spawner>();
        StartCoroutine("StartingSequence");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator StartingSequence()
    {
        announce("Survive", 3);
        yield return new WaitForSeconds(3);
        spawner.populateSpace();
    }
}
