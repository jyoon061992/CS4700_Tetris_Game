﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    private float doubleAngle = 90f;
    private float quadrupleAngle = 90f;

    void FixedUpdate()
    {
        MoveBlock();
        RotateBlock();
    }


    void MoveBlock() //should not move until figuring out if move is allowed since blocks may be in the way
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x-1, transform.position.y, transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.S)) //holding it should bring it down, but getkey is too fast, so introduce a timer to bring it down
        {
            transform.position = new Vector3(transform.position.x, Spawner.activeBlock.transform.position.y-1, Spawner.activeBlock.transform.position.z);
        }
    }


    void RotateInput() //should not rotate until figuring out if rotate is allowed since blocks may be in the way
    {
        if (gameObject.tag.Equals("nRotateBlock")) //no rotations - blocks with no rotations (O block) - for readability and exiting method every time it is attempted
        {
            return;
        } else if (gameObject.tag.Equals("dRotateBlock")) //double rotations - blocks with two states of rotations
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //if the rotate is allowed
                RotateBlock();
            }
        } else if (gameObject.tag.Equals("qRotateBlock")) //quadruple rotations - blocks with four states of rotations
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                quadrupleAngle = -90f;
                RotateBlock();
            } else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                quadrupleAngle = 90f;
                RotateBlock();
            }
        }
    }

    void RotateBlock()
    {
        if (gameObject.tag.Equals("dRotateBlock"))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.RoundToInt(transform.eulerAngles.z + doubleAngle));
            doubleAngle = -doubleAngle;
        } else if (gameObject.tag.Equals("qRotateBlock"))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.RoundToInt(transform.eulerAngles.z + quadrupleAngle));
        }
    }

}
