using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixGrid
{
    public static int row= 20;
    public static int column = 10;
    public static bool[,] grid = new bool[row, column];


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
        if (position.x >= -5 && position.x <= 5 && position.y >= -10){
            return true;
        } else{
            return false;
        }
    }




}
