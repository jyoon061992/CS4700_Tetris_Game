using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{

    private int doubleAngle = 90;
    private int quadrupleAngle = 90;
    private bool moveAllowed = false;
    private bool rotateAllowed = false;
	private float delayStart, initialDelay = 4f/15f, autoDelay = .1f, delay;
	private bool useDelay = true;

    void FixedUpdate()
    {
        MoveInput();
        RotateInput();
        StartCoroutine(CheckBlockPosition());
    }


    void MoveInput() //should not move until figuring out if move is allowed since blocks may be in the way
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
			delayStart = Time.time;
			delay = initialDelay;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
			delayStart = Time.time;
			delay = initialDelay;
        }
        else if (Input.GetKeyDown(KeyCode.S)) //holding it should bring it down, but getkey is too fast, so introduce a timer to bring it down
        {
			delayStart = Time.time;
			delay = initialDelay;
        }

		if (Input.GetKeyUp(KeyCode.A)) {
			foreach (Transform child in transform) {
				moveAllowed = !MatrixGrid.IsBlockLeft(child.position); //if there is no block to the left, move left is allowed
				if (!moveAllowed) {
					return;
				}
			}
			MoveBlockLeft();
		}
		else if (Input.GetKeyUp(KeyCode.D)) {
			foreach (Transform child in transform) {
				moveAllowed = !MatrixGrid.IsBlockRight(child.position); //if there is no block to the right, move right is allowed
				if (!moveAllowed) {
					return;
				}
			}
			MoveBlockRight();
		}
		/*else if (Input.GetKeyUp(KeyCode.S)) //holding it should bring it down, but getkey is too fast, so introduce a timer to bring it down
		{
			delayStart = Time.time;
			delay = initialDelay;
			foreach (Transform child in transform) {
				moveAllowed = !MatrixGrid.ReachedBottom(child.position);
				if (!moveAllowed) {
					return;
				}
			}
			SoftDrop();
		}*/


		if (delayStart + delay <= Time.time) {
            delayStart = Time.time;
            //delayStart = Time.time + autoDelay;
			delay = autoDelay;
			if (Input.GetKey(KeyCode.A)) {
				foreach (Transform child in transform) {
					moveAllowed = !MatrixGrid.IsBlockLeft(child.position); //if there is no block to the left, move left is allowed
					if (!moveAllowed) {
						return;
					}
				}
				MoveBlockLeft();
			} else if (Input.GetKey(KeyCode.D)) {
				foreach (Transform child in transform) {
					moveAllowed = !MatrixGrid.IsBlockRight(child.position); //if there is no block to the right, move right is allowed
					if (!moveAllowed) {
						return;
					}
				}
				MoveBlockRight();
			}
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
                quadrupleAngle = -90;
                RotateBlock();
            } else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                quadrupleAngle = 90;
                RotateBlock();
            }
        }
    }


    void MoveBlockLeft()
    {
        transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
    }

    void MoveBlockRight()
    {
        transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
    }

    void SoftDrop()
    {
        transform.position = new Vector3(transform.position.x, Spawner.activeBlock.transform.position.y - 1, Spawner.activeBlock.transform.position.z);
    }


    void RotateBlock()
    {
        if (gameObject.tag.Equals("dRotateBlock"))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.RoundToInt(transform.eulerAngles.z + (int)doubleAngle));
            doubleAngle = -doubleAngle;
        } else if (gameObject.tag.Equals("qRotateBlock"))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.RoundToInt(transform.eulerAngles.z + (int)quadrupleAngle));
        }
    }

    IEnumerator CheckBlockPosition()
    {
        foreach (Transform child in transform)
        {
            if (MatrixGrid.ReachedBottom(child.position))
            {
                Spawner.activeBlock = null;
                yield return new WaitForSeconds(BlockFallTimer.timer + (14f / 60f));
                foreach (Transform child2 in transform)
                {
                    MatrixGrid.Lock(child2.position);
                }
                CheckRowClears();
                Spawner.isBlockPlaced = true;
                GameObject.Destroy(this);
            }
        }
    }

    void CheckRowClears()
    {
        MatrixGrid.rowClears = 0;
        for (int i = 0; i < 20; i++)
        {
            if (MatrixGrid.IsRowClear(i))
            {
                MatrixGrid.rowClears++;
                Debug.Log("Row " + i + " Clear!");
                DeleteAndShift(i);
            }
        }
    }

    void DeleteAndShift(int rowNumber)
    {
        //Destroy()
    }



}
