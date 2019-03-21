using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu: MonoBehaviour
{

    bool Options= false;

    void Start()
    {

    }

    public void OptionsSwitch()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("TetrisGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
