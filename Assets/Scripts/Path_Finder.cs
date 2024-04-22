using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path_Finder : MonoBehaviour
{/*
    int Binary_insert(List<int[]> input_vec, int[] push_vec)
    {
        int sp = 0;
        int ep = input_vec.Length - 1;
        int mid = 0;

        while(sp<=ep)
        {
            mid = (sp + ep) / 2;
            if(input_vec[mid,1]+input_vec[mid,2] > push_vec[1]+push_vec[2])
            {
                ep = mid - 1;
            }
            else if (input_vec[mid, 1] + input_vec[mid, 2] < push_vec[1] + push_vec[2])
            {
                sp = mid + 1;
            }
            else
            {
                break;
            }
        }

        if(sp>ep)
        {
            mid = sp;
        }

        return mid;
    }

    void A_star_pathfind(int[,] input_map, int[] coordinate)
    {
        int sp_x = coordinate[0];
        int sp_y = coordinate[1];
        int ep_x = coordinate[2];
        int ep_y = coordinate[3];
        int idx = 0;
        int next_node_idx = 0;
        int change_value_idx = 0;
        int res_x = 0;
        int res_y = 0;
        int mid = 0;
        int f_cost = 0;
        int h_cost = 0;
        List<int[]> neighbors = new List<int[]>();
        List<int[]> route = new List<int[]>();
        int[] col=new int[7];
        int[] current=new int[2];
        int[] restore=new int[8];
        int[] next_current=new int[2];
        Dictionary<int[], int[]> info=new Dictionary<int[], int[]>();

        //start point initialize
        col[0] = 1;
        col[1] = 0;
        col[2] = Mathf.Abs(sp_x - ep_x) + Mathf.Abs(ep_x - ep_y);
        col[3] = sp_x;
        col[4] = sp_y;
        col[5] = ep_x;
        col[6] = ep_y;
        neighbors.Add(col);
        current[0] = sp_x;
        current[1] = sp_y;
        info[current] = col;

        while (current[0]!=ep_x || current[1]!=ep_y)//until arrive at goal
        {
            restore[0] = current[0] + 1;
            restore[1] = current[1];
            restore[2] = current[2] - 1;
            restore[3] = current[1];
            restore[4] = current[0];
            restore[5] = current[1] + 1;
            restore[6] = current[0];
            restore[7] = current[1] - 1;

            idx = 0;
            while(idx<8)//look for neighbors
            {
                res_x = restore[idx];
                res_y = restore[idx+1];
                next_current[0] = res_x;
                next_current[1] = res_y;

                //look for range in map size and visitable
                if(((res_x >= 0 && res_x < input_map.Length) && (res_y >= 0 && res_y < input_map.Length)) && (input_map[res_x,res_y] != 1))
                {
                    if(info[next_current]==null)
                    {
                        col[0] = 0;
                        col[1] = info[current][1] + 1;
                        col[2] = Mathf.Abs(ep_x - res_x) + Mathf.Abs(ep_y - res_y);
                        col[3] = res_x;
                        col[4] = res_y;
                        col[5] = current[0];
                        col[6] = current[1];
                        info[next_current] = col;
                        mid = Binary_insert(neighbors, col);
                    }
                }
            }
        }
    }*/
}
