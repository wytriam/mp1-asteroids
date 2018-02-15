using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu_manager : MonoBehaviour
{
    public string playScene;
    public GameObject instructions;
    public GameObject playButton;
    public GameObject instructionsButton;
    public GameObject returnButton;


	// Use this for initialization
	void Start ()
    {
        returnButton.SetActive(false);
        instructions.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void PlayButton()
    {
        SceneManager.LoadScene(playScene);
    }

    public void InstructionsButton()
    {
        playButton.SetActive(false);
        instructionsButton.SetActive(false);
        returnButton.SetActive(true);
        instructions.SetActive(true);
    }

    public void ReturnButton()
    {
        playButton.SetActive(true);
        instructionsButton.SetActive(true);
        returnButton.SetActive(false);
        instructions.SetActive(false);

    }
}
