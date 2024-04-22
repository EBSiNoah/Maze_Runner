using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze_Generator_mk1 : MonoBehaviour
{
    public GameObject[] Cell = new GameObject[4225];
    public GameObject ground;
    public GameObject wall;
    public Transform coordinate;
    private int[] binary_Cell = new int[4225];

    // Start is called before the first frame update
    void Start()
    {
        int row = 1;
        int col = 1;
        int idx = 0;
        int cnt = 2;
        int horizontal_rand = 0;
        int vertical_rand = 0;
        int restore_rand = 0;
        int boolean_rand = 0;
        Vector3 temp;

        boolean_rand = (int)Random.value % 2;

        while (idx < 4225)
        {
            binary_Cell[idx] = 0;
            idx++;
        }
        //initialize
        binary_Cell[0] = 1;
        binary_Cell[1] = 1;
        binary_Cell[2] = 1;
        binary_Cell[65] = 1;
        binary_Cell[67] = 1;
        binary_Cell[130] = 1;
        binary_Cell[131] = 1;
        binary_Cell[132] = 1;

        while (cnt < 64)
        {
            row = 0;
            while (row <= cnt)
            {
                col = 1;
                while (col <= cnt)
                {
                    binary_Cell[65 * row + cnt + col] = binary_Cell[65 * row + col];
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
                    binary_Cell[65 * (row + cnt) + col] = binary_Cell[65 * row + col];
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
                    binary_Cell[65 * (row + cnt) + cnt + col] = binary_Cell[65 * row + col];
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
            binary_Cell[65 * horizontal_rand + cnt] = 0;
            binary_Cell[65 * cnt + vertical_rand] = 0;

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
                binary_Cell[65 * restore_rand + cnt] = 0;
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
                binary_Cell[65 * cnt + restore_rand] = 0;
                boolean_rand = 1;
            }

            cnt *= 2;
        }
        
        idx = 0;
        while (idx < 4225)
        {
            temp = new Vector3((32-idx / 65) * 0.5f, 0, (32-idx % 65) * 0.5f);
            if (binary_Cell[idx] == 1)
            {
                Instantiate(wall, coordinate.position + temp, coordinate.rotation);
            }
            else if(binary_Cell[idx] == 0)
            {
                Instantiate(ground, coordinate.position + temp, coordinate.rotation);
            }
            idx++;
        }
    }

}
