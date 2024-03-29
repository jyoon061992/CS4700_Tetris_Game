﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
	public Button resume;
    bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MatrixGrid.gameOver) {
			isPaused = true;
			resume.enabled = false;
		} else {
			resume.enabled = true;
		}
    }

    public void SwitchPauseMenu()
    {
        if (isPaused)
        {
            pauseCanvas.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            pauseCanvas.SetActive(true);
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
