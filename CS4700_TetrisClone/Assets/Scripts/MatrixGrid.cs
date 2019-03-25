using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixGrid : MonoBehaviour
{
    public static int row = 20;
    public static int column = 10;
    public static bool[,] grid = new bool[column, row];
    public static GameObject[,] blockGrid = new GameObject[column, row];
    public static int rowClears = 0;
    private static bool rowCleared = false;
	public static bool gameOver = false;

	public static AudioSource source;
	public static AudioClip clear;

	public static void InitializeGrid() {
		gameOver = false;
        for (int i = 0; i < column; i++) {
            for (int j = 0; j < row; j++) {
                grid[i, j] = false;
            }
        }
    }

    /*public static bool IsWithinBoundaries(Vector3 position)
    {
        if ((position.x >= -5) && (position.x <= 4) && (position.y >= -10)) {
            return true;
        } else {
            return false;
        }
    }*/

	private static bool IsInArray(Vector3Int pos) {
		if (pos.x < 0) {
			return false;
		}

		if (pos.x > 9) {
			return false;
		}

		if (pos.y < 0) {
			return false;
		}

		if (pos.y > 19) {
			return false;
		}

		return true;
	}

	public static bool CheckPosFilled(Vector3Int pos) {
		if (IsInArray(pos)) {
			return grid[pos.x, pos.y];
		} else {
			return true;
		}
	}

    public static bool ReachedBottom(Vector3Int position) {
		if (position.y <= 0) {
			return true;
		} else {
			return grid[position.x, position.y - 1];
		}
    }

    public static void Lock(Vector3Int position) {
        grid[position.x, position.y] = true;
    }

    public static bool IsBlockLeft(Vector3Int position) {
        if (IsInArray(position + Vector3Int.left)) { 
            return grid[position.x - 1, position.y];
		} else {
            return true;
        }
    }

    public static bool IsBlockRight(Vector3Int position) {
        if (IsInArray(position + Vector3Int.right)) { 
            return grid[position.x + 1, position.y];
        } else {
            return true;
        }
    }

    public static bool IsRowClear(int rowNumber) {
		bool rowCleared = true;
        for (int i = 0; i < column; i++) {
            if (!grid[i, rowNumber]) {
                rowCleared = false;
                break;
            } else {
                rowCleared = true;
            }
        }

        if (rowCleared) {
            for (int i = 0; i < column; i++) {
                grid[i, rowNumber] = false;
                Destroy(blockGrid[i, rowNumber]);
				blockGrid[i, rowNumber] = null;
			}
            ShiftRow(rowNumber);
        }

        return rowCleared;
    }

    public static void ShiftRow(int rowNumber) {
		source.PlayOneShot(clear);
		for (int j = rowNumber; j < row; j++) {
			for (int i = 0; i < column; i++) {
				if ((j + 1) < row) {
					if (blockGrid[i, j + 1] != null) {
						grid[i, j] = grid[i, j + 1];
						blockGrid[i, j] = blockGrid[i, j + 1];
						blockGrid[i, j].transform.position = new Vector3Int(i, j, 0);
						blockGrid[i, j + 1] = null;
						grid[i, j + 1] = false;
					} else {
						blockGrid[i, j] = null;
						grid[i, j] = false;
					}
				}
            }
        }
    }

	/*public static void ShiftRow2(int rowNumber) {
		for (int i = rowNumber; i < row; i++) {
			for (int j = 0; j < column; j++) {
				if (blockGrid[i + 1, j] != null && i + 1 < row) {
					grid[i, j] = grid[i + 1, j];
					blockGrid[i, j] = blockGrid[i + 1, j];
					blockGrid[i, j].transform.position = new Vector3(j - 5, i - 10);
				}
				else {
					blockGrid[i, j] = null;
					grid[i, j] = false;
				}
			}
		}
	}*/


	public static void SetGrid(Vector3Int position, GameObject fillObject) {
        blockGrid[position.x, position.y] = fillObject;
    }
}
