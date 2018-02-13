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
    private bool ending = false;
    private bool anyKeyPressed = false;

	// Use this for initialization
	void Start ()
    {
        transition = true;
        constants = GetComponent<Constants>();
        spawner = GetComponent<Spawner>();
        StartCoroutine("StartWave");
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Check for next wave
        if (!transition && constants.getNumAsteroids() <= 0)
            StartCoroutine("EndWave");
        
        // Check for game loss
        if (!transition && !ending && constants.getNumShips() <= 0)
            StartCoroutine("EndGame");

        if(ending)
            if (Input.anyKey)
                anyKeyPressed = true;
    }

    IEnumerator StartWave()
    {
        announce("Wave " + (waveCount+1) + ":\nSurvive", 3);
        spawner.populateSpace(asteroidsPerWave);
        yield return new WaitForSeconds(3);
        spawner.spawnPlayer();
        // transition is only false after new ship is spawned
        transition = false;
        asteroidsPerWave += waveIncrement;
        StopCoroutine("StartWave");
    }

    IEnumerator EndWave()
    {
        transition = true;
        waveCount++;
        announce("Wave Complete", 3);
        yield return new WaitForSeconds(3);
        StartCoroutine("StartWave");
        StopCoroutine("EndWave");
    }

    IEnumerator EndGame()
    {
        announce("Space is a dangerous realm...", 3);
        yield return new WaitForSeconds(3);
        ending = true;
        while (!anyKeyPressed)
        {
            announce("Your fleet survived " + waveCount + " waves!\nPress any key to return to the main menu.", 3);
            yield return new WaitForSeconds(6);
        }
        Debug.Log("Returning to Main Menu...");
        // TO-DO - MENU TRANSITION
    }
}
