using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public GameObject flame;
    public string axis;
    // the flame is moving up or right
    public bool upRight;

	// Use this for initialization
	void Start ()
    {
        flame.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (upRight)
        {
            if (Input.GetAxis(axis) > 0)
                flame.SetActive(true);
            //if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            else
                flame.SetActive(false);
        }
        else
        {
            if (Input.GetAxis(axis) < 0)
                flame.SetActive(true);
            //if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            else
                flame.SetActive(false);

        }
    }
}
