using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants: MonoBehaviour
{
    [Header("Do not change during play: ")]
    [SerializeField]
    private int numShips = 0;

    [SerializeField]
    private int numAsteroids = 0;

    [SerializeField]
    private int asteroidsDestroyedCount = 0;

    public int waveCount = 0;
    public int asteroidsPerWave = 10;
    public int waveIncrement = 2;


    // initialize listeners
    void Awake()
    {
        Messenger.AddListener(Messages.INST_ASTEROID, incAst);
        Messenger.AddListener(Messages.DEST_ASTEROID, decAst);
        Messenger.AddListener(Messages.DEST_ASTEROID, astDestCnt);
        Messenger.AddListener(Messages.INST_SHIP, incShips);
        Messenger.AddListener(Messages.DEST_SHIP, decShips);
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

    void astDestCnt()
    {
        asteroidsDestroyedCount++;
    }

    public int getAsteroidsDestroyed()
    {
        return asteroidsDestroyedCount;
    }

    public int getNumAsteroids()
    {
        return numAsteroids;
    }

    // decrements the number of ships
    void decShips()
    {
        numShips--;
    }

    // increments the number of ships
    void incShips()
    {
        numShips++;
    }

    public int getNumShips()
    {
        return numShips;
    }
}