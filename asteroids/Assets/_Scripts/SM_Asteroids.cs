using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Asteroids : WytriamSTD.Scene_Manager
{

    private Constants constants;
    private Spawner spawner;
    
    [HideInInspector]
    public bool transition = false;
    [HideInInspector]
    public bool ending = false;

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
        {
            ending = true;
            Messenger.Broadcast(Messages.GAME_OVER);
        }
    }

    IEnumerator StartWave()
    {
        announce("Wave " + (constants.waveCount+1) + ":\nSurvive", 3);
        spawner.populateSpace(constants.asteroidsPerWave);
        yield return new WaitForSeconds(3);
        spawner.spawnPlayer();
        // transition is only false after new ship is spawned
        transition = false;
        constants.asteroidsPerWave += constants.waveIncrement;
        StopCoroutine("StartWave");
    }

    IEnumerator EndWave()
    {
        Messenger.Broadcast(Messages.WAVE_CLEAR);
        transition = true;
        constants.waveCount++;
        announce("Wave Complete", 3);
        yield return new WaitForSeconds(3);
        StartCoroutine("StartWave");
        StopCoroutine("EndWave");
    }
}
