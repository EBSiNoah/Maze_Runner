using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze_Generator_mk2 : MonoBehaviour
{
    public GameObject ground;
    public GameObject wall;
    public Transform coordinate;
    public int[,] binary_Cell = new int[65,65];

    // Start is called before the first frame update
    void Start()
    {
        int row = 1;
        int col = 1;
        int row_idx = 0;
        int col_idx = 0;
        int cnt = 2;
        int horizontal_rand = 0;
        int vertical_rand = 0;
        int restore_rand = 0;
        int boolean_rand = 0;
        Vector3 temp;

        boolean_rand = (int)Random.value % 2;

        while (row_idx < 65)
        {
            col_idx = 0;
            while(col_idx < 65)
            {
                binary_Cell[row_idx, col_idx] = 0;
                col_idx++;
            }
            row_idx++;
        }
        //initialize
        binary_Cell[0,0] = 1;
        binary_Cell[0,1] = 1;
        binary_Cell[0,2] = 1;
        binary_Cell[1,0] = 1;
        binary_Cell[1,2] = 1;
        binary_Cell[2,0] = 1;
        binary_Cell[2,1] = 1;
        binary_Cell[2,2] = 1;

        while (cnt < 64)
        {
            row = 0;
            while (row <= cnt)
            {
                col = 1;
                while (col <= cnt)
                {
                    binary_Cell[row, cnt + col] = binary_Cell[row, col];
                    col++;
                }
                row++;
            }

            row = 1;
            while (row <= cnt)
            {
                col = 0;
                while (col <= cnt)
                {
                    binary_Cell[(row + cnt), col] = binary_Cell[row, col];
                    col++;
                }
                row++;
            }

            row = 1;
            while (row <= cnt)
            {
                col = 1;
                while (col <= cnt)
                {
                    binary_Cell[(row + cnt), cnt + col] = binary_Cell[row, col];
                    col++;
                }
                row++;
            }

            horizontal_rand = (int)Random.Range(1, 2 * cnt);
            vertical_rand = (int)Random.Range(1, 2 * cnt);

            while (horizontal_rand % 2 == 0)
            {
                horizontal_rand = (int)Random.Range(1, 2 * cnt);
            }
            while (vertical_rand % 2 == 0)
            {
                vertical_rand = (int)Random.Range(1, 2 * cnt);
            }
            binary_Cell[horizontal_rand, cnt] = 0;
            binary_Cell[cnt, vertical_rand] = 0;

            if (boolean_rand == 1)
            {
                if (horizontal_rand > cnt)
                {
                    restore_rand = (int)Random.Range(1, cnt);
                    while (restore_rand % 2 == 0)
                    {
                        restore_rand = (int)Random.Range(1, cnt);
                    }
                }
                else
                {
                    restore_rand = (int)Random.Range(cnt + 1, 2 * cnt);
                    while (restore_rand % 2 == 0)
                    {
                        restore_rand = (int)Random.Range(cnt + 1, 2 * cnt);
                    }
                }
                binary_Cell[restore_rand, cnt] = 0;
                boolean_rand = 0;
            }
            else
            {
                if (vertical_rand > cnt)
                {
                    restore_rand = (int)Random.Range(1, cnt);
                    while (restore_rand % 2 == 0)
                    {
                        restore_rand = (int)Random.Range(1, cnt);
                    }
                }
                else
                {
                    restore_rand = (int)Random.Range(cnt + 1, 2 * cnt);
                    while (restore_rand % 2 == 0)
                    {
                        restore_rand = (int)Random.Range(cnt + 1, 2 * cnt);
                    }
                }
                binary_Cell[cnt, restore_rand] = 0;
                boolean_rand = 1;
            }

            cnt *= 2;
        }

        row_idx = 0;
        while (row_idx < 65)
        {
            col_idx = 0;
            while(col_idx < 65)
            {
                temp = new Vector3((32 - row_idx % 65) * 0.5f, 0, (32 - col_idx % 65) * 0.5f);
                if (binary_Cell[row_idx, col_idx] == 1)
                {
                    Instantiate(wall, coordinate.position + temp, coordinate.rotation);
                }
                else if (binary_Cell[row_idx, col_idx] == 0)
                {
                    Instantiate(ground, coordinate.position + temp, coordinate.rotation);
                }
                col_idx++;
            }
            row_idx++;
        }
    }
}
