using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants: MonoBehaviour
{
    [Header("Do not change during play: ")]
    public int numLives = 3;

    [SerializeField]
    private int numAsteroids = 0;

    // initialize listeners
    void Awake()
    {
        Messenger.AddListener(Messages.INST_ASTEROID, incAst);
        Messenger.AddListener(Messages.DEST_ASTEROID, decAst);
    }


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // decrements the number of asteroids
    void decAst()
    {
        numAsteroids--;
    }

    // increments the number of asteroids
    void incAst()
    {
        numAsteroids++;
    }

    public int getNumAsteroids()
    {
        return numAsteroids;
    }
}