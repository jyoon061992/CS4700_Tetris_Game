using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] blocks;
    public static GameObject activeBlock;
    private int randomBlock;
    private int oldRandomBlock;
    public static bool isBlockPlaced;
    
    void Awake()
    {
        MatrixGrid.InitializeGrid();
        oldRandomBlock = 7;
        isBlockPlaced = true;
    }

    void FixedUpdate()
    {
        SpawnRandom();
    }


    void SpawnRandom()
    {
        //pseudo random - slightly biased against a repeating piece, but only 1 reroll in original game, possibility of heavy droughts adds to the challenge
        if (isBlockPlaced) {
            /*if block is placed, begin timer/countdown in this section
            *
            *
            * 
            * 
            */
            randomBlock = Random.Range(0, blocks.Length+1);
            if (randomBlock < blocks.Length && randomBlock != oldRandomBlock)
            {
                activeBlock = Instantiate(blocks[randomBlock], transform.position, Quaternion.identity);
                isBlockPlaced = false;
                oldRandomBlock = randomBlock;
            }
            else
            {
                randomBlock = Random.Range(0, blocks.Length);
                activeBlock = Instantiate(blocks[randomBlock], transform.position, Quaternion.identity);
                isBlockPlaced = false;
                oldRandomBlock = randomBlock;
            }
        }
    }

}
