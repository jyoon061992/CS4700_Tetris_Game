using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScore : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    private int score = 0;


    void Awake()
    {
        DisplayScore();
    }

    void Update()
    {
        //SetScore();
    }


    void DisplayScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }


    void SetScore()
    {
        //based on a switch statement that tests how many lines were cleared at once from a different class
        //score += 40*level for 1 row, += 100*level for 2 rows, += 300*level for 3 rows, += 1200*level for tetris
        //if score has changed
        //DisplayScore(); to visually update the score
    }

}
