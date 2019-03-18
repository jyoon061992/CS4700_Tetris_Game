using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] blocks;
    public static GameObject activeBlock;
    int randomblock;
    bool isBlockPlaced;

    // Start is called before the first frame update
    void Awake()
    {
        isBlockPlaced = true;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnRandom();
    }


    void SpawnRandom()
    {
        if (isBlockPlaced) {
            randomblock = Random.Range(0, blocks.Length);
            activeBlock = Instantiate(blocks[randomblock], transform.position, Quaternion.identity);
            isBlockPlaced = false;
        }
    }

}
