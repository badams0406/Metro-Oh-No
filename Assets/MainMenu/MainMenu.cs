using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider slider;

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StopGame() {
        Application.Quit();
    }

    private void Update()
    {
        FindObjectOfType<AudioManager>().adjustVolume(slider.value);
    }
    
}
