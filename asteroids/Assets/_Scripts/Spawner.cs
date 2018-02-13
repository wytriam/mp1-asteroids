using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject player;
    public int numAsteroids = 10;
    public Vector2 astSpawnX = new Vector2(-100, 100);
    public Vector2 astSpawnY = new Vector2(-100, 100);
    public GameObject[] asteroids;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void spawnPlayer()
    {
        Instantiate<GameObject>(player);
    }

    // creates the asteroid field
    public void populateSpace(int numAsteroids)
    {
        for (int i=0; i < numAsteroids; i++)
        { 
            int index = 0;
            float rand = Random.value;
            if (rand >= 0.9f)   // 10% chance for small asteroid
                index = 2;
            if (rand >= 0.7f)   // 20% chance for medium asteroid
                index = 1;
            GameObject ast = Instantiate<GameObject>(asteroids[index]);
            ast.transform.position = new Vector3(Random.Range(astSpawnX.x, astSpawnX.y), Random.Range(astSpawnY.x, astSpawnY.y), 0);
        }
    }
}
