using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Asteroids : WytriamSTD.Scene_Manager
{
    public int asteroidsPerWave = 10;
    public int waveIncrement = 2;

    private Constants constants;
    private Spawner spawner;
    private int waveCount = 0;
    private bool transition = false;

	// Use this for initialization
	void Start ()
    {
        constants = GetComponent<Constants>();
        spawner = GetComponent<Spawner>();
        StartCoroutine("StartWave");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!transition && constants.getNumAsteroids() <= 0)
        {
            Debug.Log("No Asteroids Remain");
            StartCoroutine("EndWave");
        }
        else
            Debug.Log("" + constants.getNumAsteroids() + " asteroids remain");
	}

    IEnumerator StartWave()
    {
        waveCount++;
        announce("Wave " + waveCount + ":\nSurvive", 3);
        spawner.populateSpace(asteroidsPerWave);
        yield return new WaitForSeconds(3);
        transition = false;
        spawner.spawnPlayer();
        asteroidsPerWave += waveIncrement;
        StopCoroutine("StartWave");
    }

    IEnumerator EndWave()
    {
        Debug.Log("EndWave::Line 49");
        transition = true;
        Debug.Log("EndWave::Line 51");
        announce("Wave Complete", 3);
        Debug.Log("EndWave::Line 53");
        yield return new WaitForSeconds(3);
        Debug.Log("EndWave::Line 55");
        StartCoroutine("StartWave");
        Debug.Log("EndWave::Line 57");
        StopCoroutine("EndWave");
    }
}
