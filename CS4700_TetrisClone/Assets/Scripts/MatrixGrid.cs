using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixGrid : MonoBehaviour
{
    public static int row = 20;
    public static int column = 10;
    public static bool[,] grid = new bool[row, column];
    public static GameObject[,] blockGrid = new GameObject[row, column];
    public static int rowClears = 0;
    private static bool rowCleared = false;

    //-10 to 9 --> 0-19, -5 to 4 --> 0-9
    private static float ConvertArrayX(float positionx)
    {
        positionx += 5;
        return positionx;
    }

    private static float ConvertArrayY(float positiony)
    {
        positiony += 10;
        return positiony;
    }


    public static void InitializeGrid()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                grid[i, j] = false;
            }
        }
    }

    public static bool IsWithinBoundaries(Vector3 position)
    {
        if ((position.x >= -5) && (position.x <= 4) && (position.y >= -10))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

	private static bool IsInArray(Vector3 pos) {
		if ((pos.x + 4) < 0) {
			return false;
		}

		if ((pos.x + 4) >= 9) {
			return false;
		}

		if ((pos.y + 10) < 0) {
			return false;
		}

		if ((pos.y + 10) >= 20) {
			return false;
		}

		return true;
	}

	public static bool CheckPosFilled(Vector3 pos) {
		if (IsInArray(pos)) {
			return grid[(int)(pos.y + 10f), (int)(pos.x + 4f)];
		} else {
			return true;
		}
	}

    public static bool ReachedBottom(Vector3 position)
    {
        if (position.y == -10 || grid[(int)ConvertArrayY(position.y - 1), (int)ConvertArrayX(position.x)])
        {
            grid[(int)ConvertArrayY(position.y), (int)ConvertArrayX(position.x)] = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void Lock(Vector3 position)
    {
        grid[(int)ConvertArrayY(position.y), (int)ConvertArrayX(position.x)] = true;
    }

    public static bool IsBlockLeft(Vector3 position)
    {
        if (grid[(int)ConvertArrayY(position.y), (int)ConvertArrayX(position.x - 1)] && IsWithinBoundaries(position))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsBlockRight(Vector3 position)
    {
        if (grid[(int)ConvertArrayY(position.y), (int)ConvertArrayX(position.x + 1)] && IsWithinBoundaries(position))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsRowClear(int rowNumber)
    {
        for (int j = 0; j < column; j++)
        {
            if (!grid[rowNumber, j])
            {
                rowCleared = false;
                break;
            }
            else
            {
                rowCleared = true;
            }
        }
        if (rowCleared)
        {
            for (int j = 0; j < column; j++)
            {
                grid[rowNumber, j] = false;
                Destroy(blockGrid[rowNumber, j].gameObject);
            }
            ShiftRow(rowNumber);
        }
        return rowCleared;
    }

    public static void ShiftRow(int rowNumber)
    {
        for (int i = rowNumber; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                grid[i, j] = grid[i + 1, j];
                blockGrid[i, j] = blockGrid[i + 1, j];
                blockGrid[i, j].transform.position = new Vector3(j - 5, i - 11);
            }
        }
    }


    public static void SetGrid(Vector3 position, GameObject fillObject)
    {
        blockGrid[(int)ConvertArrayY(position.y), (int)ConvertArrayX(position.x)] = fillObject;
    }



}
