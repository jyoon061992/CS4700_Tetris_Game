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
    private string boolArray;
    private float timer = 1.6f;

	public AudioSource source;
	public AudioClip clip;

	void Awake()
    {
        MatrixGrid.InitializeGrid();
        oldRandomBlock = 7;
        isBlockPlaced = true;
        boolArray = "";

		MatrixGrid.source = source;
		MatrixGrid.clear = clip;
    }

    void FixedUpdate()
    {

        SpawnRandom();
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            PrintToConsoleTest();
            timer = 1.6f;
        }
    }


    void SpawnRandom()
    {
        //pseudo random - slightly biased against a repeating piece, but only 1 reroll in original game, possibility of heavy droughts adds to the challenge
        if (isBlockPlaced) {
            activeBlock = null;
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

    void PrintToConsoleTest()
    {
        /*for (int i = 0; i < MatrixGrid.column; i++)
        {
            for (int j = 0; j < MatrixGrid.row; j++)
            {
                int integer = MatrixGrid.grid[i, j] ? 1 : 0;
                boolArray += integer.ToString() + " ";
            }
            boolArray += "\n";
        }*/
        //Debug.Log(boolArray + "\n\n DONE \n\n");
        //boolArray = "";
    }

    /*void PrintToConsoleTest()
    {
        for (int i = 0; i < MatrixGrid.row; i++)
        {
            for (int j = 0; j < MatrixGrid.column; j++)
            {
                int integer = MatrixGrid.blockGrid[i, j] != null? 1 : 0;
                boolArray += integer.ToString() + " ";
            }
            boolArray += "\n";
        }
        Debug.Log(boolArray + "\n\n DONE \n\n");
        boolArray = "";
    }*/

}
