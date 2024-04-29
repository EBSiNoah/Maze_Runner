using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path_Finder_mk2 : MonoBehaviour
{
    public Maze_Generator_mk3 Maze;

    int Binary_insert(List<List<int>> neighbors, List<int> current)
    {
        int sp = 0;
        int ep = neighbors.Count - 1;
        int mid = 0;

        while (sp <= ep)
        {
            mid = (sp + ep) / 2;
            if (neighbors[mid][1] + neighbors[mid][2] > current[1] + current[2])
            {
                ep = mid - 1;
            }
            else if (neighbors[mid][1] + neighbors[mid][2] < current[1] + current[2])
            {
                sp = mid + 1;
            }
            else
            {
                break;
            }
        }

        if (sp > ep)
        {
            mid = sp;
        }

        return mid;
    }

    List<int> StrConvert(string current)
    {
        List<int> output = new List<int>();
        int idx = 0;
        int x = 0;
        int y = 0;

        while (current[idx] != ',')
        {
            x = x * 10 + current[idx] - '0';
            idx++;
        }
        idx++;
        while (idx < current.Length)
        {
            y = y * 10 + current[idx] - '0';
            idx++;
        }
        output.Add(x);
        output.Add(y);
        return output;
    }

    List<List<int>> A_star_pathfind(List<List<int>> input_map, int[] coordinate)
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
        List<List<int>> neighbors = new List<List<int>>();
        List<List<int>> route = new List<List<int>>();
        List<int> col = new List<int>();
        List<int> empty_col = new List<int>();
        List<int> convert_current = new List<int>();
        int[] restore = new int[8];
        string current;
        string next_current;
        Dictionary<string, List<int>> info = new Dictionary<string, List<int>>();

        //start point initialize
        col.Add(1);
        col.Add(0);
        col.Add(Mathf.Abs(sp_x - ep_x) + Mathf.Abs(ep_x - ep_y));
        col.Add(sp_x);
        col.Add(sp_y);
        col.Add(ep_x);
        col.Add(ep_y);
        neighbors.Add(col);
        current = sp_x + "," + sp_y;
        info.Add(current, col);
        convert_current.Add(sp_x);
        convert_current.Add(sp_y);

        while (convert_current[0] != ep_x || convert_current[1] != ep_y)//until arrive at goal
        {
            restore[0] = convert_current[0] + 1;
            restore[1] = convert_current[1];
            restore[2] = convert_current[0] - 1;
            restore[3] = convert_current[1];
            restore[4] = convert_current[0];
            restore[5] = convert_current[1] + 1;
            restore[6] = convert_current[0];
            restore[7] = convert_current[1] - 1;

            idx = 0;
            while (idx < 8)//look for neighbors
            {
                res_x = restore[idx];
                res_y = restore[idx + 1];
                next_current = res_x + "," + res_y;
                //Debug.Log(current);
                //Debug.Log(next_current);

                //look for range in map size and visitable
                if (((res_x >= 0 && res_x < input_map.Count) && (res_y >= 0 && res_y < input_map.Count)) && (input_map[res_x][res_y] != 1))
                {
                    //Debug.Log(idx);
                    if (!info.TryGetValue(next_current, out empty_col))//if empty dictionary
                    {
                        Debug.Log("it's empty");
                        col[0] = 0;
                        col[1] = info[current][1] + 1;
                        col[2] = Mathf.Abs(ep_x - res_x) + Mathf.Abs(ep_y - res_y);
                        col[3] = res_x;
                        col[4] = res_y;
                        col[5] = convert_current[0];
                        col[6] = convert_current[1];
                        info.Add(next_current, col);
                        mid = Binary_insert(neighbors, col);
                        neighbors.Insert(mid, col);
                    }
                    else if (/*info.TryGetValue(next_current, out empty_col)*/info[next_current] != null && info[next_current][1] > info[current][1] + 1)//not empty and shorter way
                    {
                        Debug.Log("it's not empty");
                        change_value_idx = 0;
                        while (neighbors[change_value_idx][3] != res_x || neighbors[change_value_idx][4] != res_y)
                        {
                            change_value_idx++;
                        }
                        neighbors.RemoveAt(change_value_idx);
                        info[next_current][1] = info[current][1] + 1;
                        info[next_current][5] = convert_current[0];
                        info[next_current][6] = convert_current[1];
                        mid = Binary_insert(neighbors, info[next_current]);
                        neighbors.Insert(mid, info[next_current]);
                    }
                }
                idx += 2;
            }

            //move current
            idx = 0;
            while (idx < neighbors.Count)
            {
                if (neighbors[idx][0] != 1)
                {
                    f_cost = neighbors[idx][1] + neighbors[idx][2];
                    break;
                }
                idx++;
            }

            //no way arrive to goal
            if (idx == neighbors.Count)
            {
                return route;
            }

            h_cost = neighbors[idx][2];
            next_node_idx = idx;
            while (idx < neighbors.Count && neighbors[idx][1] + neighbors[idx][2] == f_cost)
            {
                if (neighbors[idx][2] < h_cost && neighbors[idx][0] == 0)
                {
                    h_cost = neighbors[idx][2];
                    next_node_idx = idx;
                }
                idx++;
            }

            current = neighbors[next_node_idx][3] + "," + neighbors[next_node_idx][4];
            convert_current = StrConvert(current);
            neighbors[next_node_idx][0] = 1;
            info[current][0] = 1;
            Debug.Log("next");
        }

        //route
        route.Insert(0, convert_current);
        while (convert_current[0] != sp_x || convert_current[1] != sp_y)
        {
            res_x = info[current][5];
            res_y = info[current][6];
            current = res_x + "," + res_y;
            convert_current = StrConvert(current);
            route.Insert(0, convert_current);
        }

        return route;
    }

    void Start()
    {
        int row_idx = 0;
        int col_idx = 0;
        List<List<int>> Binary_Cell = new List<List<int>>();
        List<List<int>> route = new List<List<int>>();
        int[] coordinate = new int[4];
        string restore;

        coordinate[0] = 1;
        coordinate[1] = 1;
        coordinate[2] = 63;
        coordinate[3] = 63;

        while(row_idx<65)
        {
            Binary_Cell.Add(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            row_idx++;
        }

        row_idx = 0;
        while(row_idx<Maze.binary_Cell.Count)
        {
            col_idx = 0;
            while(col_idx< Maze.binary_Cell.Count)
            {
                Binary_Cell[row_idx][col_idx] = Maze.binary_Cell[row_idx][col_idx];
                col_idx++;
            }
            row_idx++;
        }

        Debug.Log(Maze.binary_Cell[0][0]);
        route = A_star_pathfind(Binary_Cell, coordinate);

        row_idx = 0;
        while (row_idx < route.Count)
        {
            restore = route[row_idx][0].ToString() + ", " + route[row_idx][1].ToString();
            Debug.Log(restore);
            row_idx++;
        }
    }
}