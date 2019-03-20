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
	private Matrix4x4 rot;
	private Vector4 c1, c2, c3, c4;
	private float rads = Mathf.Deg2Rad * 90f;
	private float curRot = 0;

	private void Start() {
		c1 = new Vector4(Mathf.Cos(rads), Mathf.Sin(rads), 0f, 0f);
		c2 = new Vector4(-Mathf.Sin(rads), Mathf.Cos(rads), 0f, 0f);
		c3 = new Vector4(0, 0, 1, 0);
		c4 = new Vector4(0, 0, 0, 1);
		rot = new Matrix4x4(c1, c2, c3, c4);
	}

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
				curRot = 90;

				c1 = new Vector4(Mathf.Cos(Mathf.Deg2Rad * curRot), Mathf.Sin(Mathf.Deg2Rad * curRot), 0f, 0f);
				c2 = new Vector4(-Mathf.Sin(Mathf.Deg2Rad * curRot), Mathf.Cos(Mathf.Deg2Rad * curRot), 0f, 0f);
				c3 = new Vector4(0, 0, 1, 0);
				c4 = new Vector4(0, 0, 0, 1);
				rot = new Matrix4x4(c1, c2, c3, c4);
				RotateBlock();
            }
        } else if (gameObject.tag.Equals("qRotateBlock")) //quadruple rotations - blocks with four states of rotations
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
				curRot = -90;

				c1 = new Vector4(Mathf.Cos(Mathf.Deg2Rad * curRot), Mathf.Sin(Mathf.Deg2Rad * curRot), 0f, 0f);
				c2 = new Vector4(-Mathf.Sin(Mathf.Deg2Rad * curRot), Mathf.Cos(Mathf.Deg2Rad * curRot), 0f, 0f);
				c3 = new Vector4(0, 0, 1, 0);
				c4 = new Vector4(0, 0, 0, 1);
				rot = new Matrix4x4(c1, c2, c3, c4);
				RotateBlock();
			} else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                curRot = 90;
				c1 = new Vector4(Mathf.Cos(Mathf.Deg2Rad * curRot), Mathf.Sin(Mathf.Deg2Rad * curRot), 0f, 0f);
				c2 = new Vector4(-Mathf.Sin(Mathf.Deg2Rad * curRot), Mathf.Cos(Mathf.Deg2Rad * curRot), 0f, 0f);
				c3 = new Vector4(0, 0, 1, 0);
				c4 = new Vector4(0, 0, 0, 1);
				rot = new Matrix4x4(c1, c2, c3, c4);
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
		bool moveAllowed = true; 

		foreach (Transform child in transform) {
			Vector3 tempPos = transform.position + (Vector3)(rot * child.localPosition);
			moveAllowed = !MatrixGrid.CheckPosFilled(tempPos);
			//Debug.Log(tempPos + " " + (Vector3)(rot * child.localPosition));

			if (!moveAllowed) {
				return;
			}
		}

		if (gameObject.tag.Equals("dRotateBlock")) {
			transform.Rotate(Vector3.forward, curRot);
			//doubleAngle = -doubleAngle;
		}
		else if (gameObject.tag.Equals("qRotateBlock")) {
			transform.Rotate(Vector3.forward, curRot);
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
