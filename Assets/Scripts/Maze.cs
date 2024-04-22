using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public GameObject[] Cell;
    private int[] binary_Cell = new int[289];
    
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

        boolean_rand = (int)Random.value % 2;

        while (idx<289)
        {
            binary_Cell[idx]=0;
            idx++;
        }
        //initialize
        binary_Cell[0] = 1;
        binary_Cell[1] = 1;
        binary_Cell[2] = 1;
        binary_Cell[17] = 1;
        binary_Cell[19] = 1;
        binary_Cell[34] = 1;
        binary_Cell[35] = 1;
        binary_Cell[36] = 1;

        while(cnt<16)
        {
            row = 0;
            while(row<=cnt)
            {
                col = 1;
                while(col<=cnt)
                {
                    binary_Cell[17 * row + cnt + col] = binary_Cell[17 * row + col];
                    col++;
                }
                row++;
            }

            row = 1;
            while(row<=cnt)
            {
                col = 0;
                while(col<=cnt)
                {
                    binary_Cell[17 * (row + cnt) + col] = binary_Cell[17 * row + col];
                    col++;
                }
                row++;
            }

            row = 1;
            while(row<=cnt)
            {
                col = 1;
                while(col<=cnt)
                {
                    binary_Cell[17 * (row + cnt) + cnt + col] = binary_Cell[17 * row + col];
                    col++;
                }
                row++;
            }

            horizontal_rand = (int)Random.Range(1,2*cnt);
            vertical_rand = (int)Random.Range(1, 2*cnt);
            
            while(horizontal_rand%2==0)
            {
                horizontal_rand = (int)Random.Range(1, 2 * cnt);
            }
            while(vertical_rand%2==0)
            {
                vertical_rand = (int)Random.Range(1, 2 * cnt);
            }
            binary_Cell[17 * horizontal_rand + cnt] = 0;
            binary_Cell[17 * cnt + vertical_rand] = 0;

            if(boolean_rand==1)
            {
                if(horizontal_rand>cnt)
                {
                    restore_rand = (int)Random.Range(1, cnt);
                    while(restore_rand%2==0)
                    {
                        restore_rand = (int)Random.Range(1, cnt);
                    }
                }
                else
                {
                    restore_rand = (int)Random.Range(cnt+1, 2*cnt);
                    while (restore_rand % 2 == 0)
                    {
                        restore_rand = (int)Random.Range(cnt+1, 2*cnt);
                    }
                }
                binary_Cell[17 * restore_rand + cnt] = 0;
                boolean_rand = 0;
            }
            else
            {
                if(vertical_rand>cnt)
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
                binary_Cell[17 * cnt + restore_rand] = 0;
                boolean_rand = 1;
            }

            cnt*=2;
        }

        idx = 0;
        while(idx<289)
        {
            if(binary_Cell[idx]==1)
            {
                Cell[idx].transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                Cell[idx].transform.GetChild(0).gameObject.SetActive(true);
            }
            idx++;
        }
    }

}
