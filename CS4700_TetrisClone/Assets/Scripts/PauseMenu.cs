using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseCanvas;
    bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchPauseMenu()
    {
        if (isPaused)
        {
            pauseCanvas.enabled = false;
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            pauseCanvas.enabled = true;
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
