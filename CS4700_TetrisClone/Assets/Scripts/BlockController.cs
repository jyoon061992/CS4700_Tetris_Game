using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    private bool moveAllowed = false;
    private bool rotateAllowed = false;
	private float delayStart, initialDelay = 4f/15f, autoDelay = .1f, delay;
	private bool useDelay = true;
	private float curRot = 0;

	private bool valid = true;

	void FixedUpdate()
    {
		CheckBlockPosition();
	}

	private void Update() {
		MoveInput();
		RotateInput();
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
				//if there is no block to the left, move left is allowed
				if (MatrixGrid.IsBlockLeft(Vector3Int.RoundToInt(child.position))) {
					return;
				}
			}
			MoveBlockLeft();
		}
		else if (Input.GetKeyUp(KeyCode.D)) {
			foreach (Transform child in transform) {
				//if there is no block to the right, move right is allowed
				if (MatrixGrid.IsBlockRight(Vector3Int.RoundToInt(child.position))) {
					return;
				}
			}
			MoveBlockRight();
		}


		if (delayStart + delay <= Time.time) {
            delayStart = Time.time;
            //delayStart = Time.time + autoDelay;
			delay = autoDelay;
			if (Input.GetKey(KeyCode.A)) {
				foreach (Transform child in transform) {
					//if there is no block to the left, move left is allowed
					if (MatrixGrid.IsBlockLeft(Vector3Int.RoundToInt(child.position))) {
						return;
					}
				}
				MoveBlockLeft();
			} else if (Input.GetKey(KeyCode.D)) {
				foreach (Transform child in transform) {
					//if there is no block to the right, move right is allowed
					if (MatrixGrid.IsBlockRight(Vector3Int.RoundToInt(child.position))) {
						return;
					}
				}
				MoveBlockRight();
			} else if (Input.GetKey(KeyCode.S)) {
				foreach (Transform child in transform) {
					if (MatrixGrid.ReachedBottom(Vector3Int.RoundToInt(child.position))) {
						return;
					}
				}
				SoftDrop();
			}
		}
    }


    void RotateInput() //should not rotate until figuring out if rotate is allowed since blocks may be in the way
    {
		curRot = 0;
        if (gameObject.tag.Equals("nRotateBlock")) //no rotations - blocks with no rotations (O block) - for readability and exiting method every time it is attempted
        {
            return;
        } else if (gameObject.tag.Equals("dRotateBlock")) //double rotations - blocks with two states of rotations
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
				curRot = -90;
			}
				
			if(Input.GetKeyDown(KeyCode.LeftArrow)) {
				//if the rotate is allowed
				curRot = 90;
			}

			if (curRot != 0) {
				RotateBlock();
			}
            
        } else if (gameObject.tag.Equals("qRotateBlock")) //quadruple rotations - blocks with four states of rotations
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
				curRot = -90;
				RotateBlock();
			} else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                curRot = 90;
				RotateBlock();
			}
		}
    }

    void MoveBlockLeft()
    {
		transform.position += Vector3Int.left;
    }

    void MoveBlockRight()
    {
		transform.position += Vector3Int.right;
    }

    void SoftDrop()
    {
		transform.position += Vector3Int.down;
    }


    void RotateBlock()
    {
		transform.Rotate(Vector3.forward, curRot);
		foreach (Transform child in transform) {
			if(MatrixGrid.CheckPosFilled(Vector3Int.RoundToInt(child.position))) {
				transform.Rotate(Vector3.forward, -curRot);
				return;
			}
		}
	}

	void CheckBlockPosition() {
		if (delayStart + delay > Time.time) {

			valid = true;
			foreach (Transform child in transform) {
				if (MatrixGrid.CheckPosFilled(Vector3Int.RoundToInt(child.position))) {
					MatrixGrid.gameOver = true;
					return;
				}
				if (MatrixGrid.ReachedBottom(Vector3Int.RoundToInt(child.position))) {
					valid = false;
				}
			}
			if (valid)
				SoftDrop();

			foreach (Transform child in transform) {
				if (MatrixGrid.ReachedBottom(Vector3Int.RoundToInt(child.position))) {

					Spawner.activeBlock = null;
					//yield return new WaitForSeconds(BlockFallTimer.timer + (14f / 60f));
					foreach (Transform child2 in transform) {
						MatrixGrid.Lock(Vector3Int.RoundToInt(child2.position));
						MatrixGrid.SetGrid(Vector3Int.RoundToInt(child2.position), child2.gameObject);
					}

					CheckRowClears();
					Spawner.isBlockPlaced = true;
					Destroy(this);
					break;
				}
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
            }
        }
    }
}
