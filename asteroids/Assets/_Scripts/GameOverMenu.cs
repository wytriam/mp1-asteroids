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

    void Awake()
    {
        Messenger.AddListener(Messages.GAME_OVER, GameOver);

    }

    void Start()
    {
        CloseStatsButton();
        menu.SetActive(false);
    }

    void GameOver()
    {
        menu.SetActive(true);
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
