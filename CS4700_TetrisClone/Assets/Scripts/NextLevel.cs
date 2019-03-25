using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{


    public void NextLevelLoad()
    {
		Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

}
