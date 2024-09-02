using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWon : MonoBehaviour
{
    public GameObject GameWonPanel;
    public bool isPaused;
    public AntitodeScore antiPoints;


    void Start()
    {
        GameWonPanel.SetActive(false);
        isPaused = false;
      
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused && antiPoints.GetScore() == 4)
            PauseGame();
    }

    public void PauseGame()
    {
        GameWonPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        GameWonPanel.SetActive(false);
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
