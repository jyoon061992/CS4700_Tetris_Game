using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScore : MonoBehaviour
{

    public static TextMeshProUGUI scoreText;
    public static TextMeshProUGUI levelText;
    public static int score = 0;


    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        levelText = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        DisplayScore();
    }

    void Update()
    {
        //SetScore();
    }

    static void DisplayScore()
    {
        scoreText.text = "Score: " + score.ToString("000000");
        levelText.text = "Level: " + BlockFallTimer.level.ToString();
    }


    public static void SetScore()
    {


        if(score > 999999)
        {
            score = 999999;
            DisplayScore();
            return;
        } else if(score == 999999)
        {
            return;
        }
        //based on a switch statement that tests how many lines were cleared at once from a different class
        //score += 40*level for 1 row, += 100*level for 2 rows, += 300*level for 3 rows, += 1200*level for tetris
        //if score has changed
        //DisplayScore(); to visually update the score
    }

}
