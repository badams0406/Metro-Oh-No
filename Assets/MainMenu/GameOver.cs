using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverPanel;
    public bool isPaused;
    public  GameObject player;
    public  GameObject aiComp;
   

    // Start is called before the first frame update
    void Start()
    {
        GameOverPanel.SetActive(false);
        isPaused = false;
        player = GameObject.Find("Player");
        aiComp = GameObject.Find("AI Companion");
    }


    // Update is called once per frame
    void Update()
    {
        if (!isPaused && (player == null || aiComp == null))
            PauseGame();
        
    }

    public void PauseGame()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        GameOverPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
