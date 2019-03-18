using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    void FixedUpdate()
    {
        MoveBlock();
        RotateBlock();
    }


    void MoveBlock() //should actually not move until figuring out if move is allowed since blocks may be in the way
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Spawner.activeBlock.transform.position = new Vector3(Spawner.activeBlock.transform.position.x-1, Spawner.activeBlock.transform.position.y, Spawner.activeBlock.transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Spawner.activeBlock.transform.position = new Vector3(Spawner.activeBlock.transform.position.x + 1, Spawner.activeBlock.transform.position.y, Spawner.activeBlock.transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.S)) //holding it should bring it down, but getkey is too fast, so introduce a timer to bring it down
        {
            Spawner.activeBlock.transform.position = new Vector3(Spawner.activeBlock.transform.position.x, Spawner.activeBlock.transform.position.y-1, Spawner.activeBlock.transform.position.z);
        }
        //hard drops don't exist in original tetris, so no W is used
    }


    void RotateBlock() //should actually not rotate every block such as the O block and until figuring out if rotate is allowed since blocks may be in the way
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Spawner.activeBlock.transform.eulerAngles = new Vector3(Spawner.activeBlock.transform.eulerAngles.x, Spawner.activeBlock.transform.eulerAngles.y, (int)Spawner.activeBlock.transform.eulerAngles.z+90);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Spawner.activeBlock.transform.eulerAngles = new Vector3(Spawner.activeBlock.transform.eulerAngles.x, Spawner.activeBlock.transform.eulerAngles.y, (int)Spawner.activeBlock.transform.eulerAngles.z-90);
        }
    }

    //cannot hold a block in the original tetris

}
