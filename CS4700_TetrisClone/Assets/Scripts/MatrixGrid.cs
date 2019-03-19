using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixGrid: MonoBehaviour
{
    public static int row= 20;
    public static int column = 10;
    public static bool[,] grid = new bool[row, column];

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
        if (position.x >= -5 && position.x <= 4 && position.y >= -10){
            return true;
        } else{
            return false;
        }
    }

    public static bool ReachedBottom(Vector3 position)
    {
        if (position.y == -10 || grid[(int)ConvertArrayY(position.y-1), (int)ConvertArrayX(position.x)])
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
        if (grid[(int)ConvertArrayY(position.y), (int)ConvertArrayX(position.x-1)] && IsWithinBoundaries(position))
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

    public static void ClearRows()
    {

    }

}
