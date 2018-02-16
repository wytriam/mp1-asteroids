using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public string MainMenuScene = "menu";
    public GameObject menu;
    public GameObject gameOverText;
    public GameObject mainMenuButton;
    public GameObject openStatsButton;
    public GameObject closeStatsButton;
    public GameObject stats;

    private GameObject selectButton = null;
    private bool menuActivated = true;

    void Awake()
    {
        Messenger.AddListener(Messages.GAME_OVER, GameOver);
    }

    void Start()
    {
        CloseStatsButton();
        menuActivated = false;
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!menuActivated) return;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            SelectButton();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (selectButton == null)
                SelectButton();
            selectButton.GetComponent<Button>().onClick.Invoke();
        }
    }

    void SelectButton()
    {
        if (selectButton == null)
        {
            if (openStatsButton.activeInHierarchy)
                selectButton = openStatsButton;
            else
                selectButton = closeStatsButton;
        }
        else if (selectButton == mainMenuButton)
        {
            if (openStatsButton.activeInHierarchy)
                selectButton = openStatsButton;
            else
                selectButton = closeStatsButton;
        }
        else if (selectButton == openStatsButton || selectButton == closeStatsButton)
            selectButton = mainMenuButton;

        selectButton.GetComponent<Button>().Select();

    }

    void GameOver()
    {
        menu.SetActive(true);
        menuActivated = true;
        Constants c = GetComponent<Constants>();
        Text statText = stats.GetComponent<Text>();
        statText.text = "Waves Cleared: " + c.waveCount + "\n" +
                        "Asteroids Destroyed: " + c.getAsteroidsDestroyed() + "\n" +
                        "Asteroids Remaining: " + c.getNumAsteroids();

    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(MainMenuScene);
    }

    public void OpenStatsButton()
    {
        gameOverText.SetActive(false);
        openStatsButton.SetActive(false);
        closeStatsButton.SetActive(true);
        stats.SetActive(true);
    }

    public void CloseStatsButton()
    {
        gameOverText.SetActive(true);
        openStatsButton.SetActive(true);
        closeStatsButton.SetActive(false);
        stats.SetActive(false);
    }
}
