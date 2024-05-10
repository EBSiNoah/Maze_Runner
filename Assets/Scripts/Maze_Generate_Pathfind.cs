using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze_Generate_Pathfind : MonoBehaviour
{
    public GameObject ground;
    public GameObject wall;
    public GameObject route_obj;
    public Transform coordinate;
    public List<List<int>> binary_Cell = new List<List<int>>();
    public GameObject robot;
    public float robot_speed;
    private List<List<int>> route;
    private int route_idx;
    //private float draw_idx;

    List<List<int>> MazeMaker()
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
        List<int> Cell_col = new List<int>();

        boolean_rand = (int)Random.value % 2;

        while (row_idx < 65)
        {
            binary_Cell.Add(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            row_idx++;
        }
        //initialize
        binary_Cell[0][0] = 1;
        binary_Cell[0][1] = 1;
        binary_Cell[0][2] = 1;
        binary_Cell[1][0] = 1;
        binary_Cell[1][2] = 1;
        binary_Cell[2][0] = 1;
        binary_Cell[2][1] = 1;
        binary_Cell[2][2] = 1;

        while (cnt < 64)//Tessellation algorithm
        {
            row = 0;
            while (row <= cnt)//copy to right
            {
                col = 1;
                while (col <= cnt)
                {
                    binary_Cell[row][cnt + col] = binary_Cell[row][col];
                    col++;
                }
                row++;
            }

            row = 1;
            while (row <= cnt)//copy to down
            {
                col = 0;
                while (col <= cnt)
                {
                    binary_Cell[(row + cnt)][col] = binary_Cell[row][col];
                    col++;
                }
                row++;
            }

            row = 1;
            while (row <= cnt)//copy to diagonal
            {
                col = 1;
                while (col <= cnt)
                {
                    binary_Cell[(row + cnt)][cnt + col] = binary_Cell[row][col];
                    col++;
                }
                row++;
            }

            //make hole inside 3 points
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
            binary_Cell[horizontal_rand][cnt] = 0;
            binary_Cell[cnt][vertical_rand] = 0;

            if (boolean_rand == 1)//additional horizontal hole
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
                binary_Cell[restore_rand][cnt] = 0;
                boolean_rand = 0;
            }
            else//additional vertical hole
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
                binary_Cell[cnt][restore_rand] = 0;
                boolean_rand = 1;
            }

            cnt *= 2;
        }

        row_idx = 0;
        while (row_idx < 65)
        {
            col_idx = 0;
            while (col_idx < 65)
            {
                temp = new Vector3((32 - row_idx % 65) * 0.5f, 0, (32 - col_idx % 65) * 0.5f);
                if (binary_Cell[row_idx][col_idx] == 1)
                {
                    Instantiate(wall, coordinate.position + temp, coordinate.rotation);
                }
                else if (binary_Cell[row_idx][col_idx] == 0)
                {
                    Instantiate(ground, coordinate.position + temp, coordinate.rotation);
                }
                col_idx++;
            }
            row_idx++;
        }

        return binary_Cell;
    }

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

    List<List<int>> A_star_pathfind(List<List<int>> input_map, int[] input_coordinate)
    {
        int sp_x = input_coordinate[0];
        int sp_y = input_coordinate[1];
        int ep_x = input_coordinate[2];
        int ep_y = input_coordinate[3];
        int idx = 0;
        int next_node_idx = 0;
        int change_value_idx = 0;
        int res_x = 0;
        int res_y = 0;
        int mid = 0;
        int f_cost = 0;
        int h_cost = 0;
        List<List<int>> neighbors = new List<List<int>>();
        List<List<int>> result_route = new List<List<int>>();
        List<int> col = new List<int>();
        List<int> empty_col = new List<int>();
        List<int> convert_current = new List<int>();
        int[] restore = new int[8];
        string current;
        string next_current;
        Dictionary<string, List<int>> info = new Dictionary<string, List<int>>();

        //Vector3 temp;

        //start point initialize
        col.Add(1);
        col.Add(0);
        col.Add(Mathf.Abs(sp_x - ep_x) + Mathf.Abs(sp_x - ep_y));
        col.Add(sp_x);
        col.Add(sp_y);
        col.Add(sp_x);
        col.Add(sp_y);
        neighbors.Add(col);
        current = sp_x + "," + sp_y;
        info.Add(current, new List<int> { 1,0,Mathf.Abs(sp_x - ep_x) + Mathf.Abs(sp_x - ep_y),sp_x,sp_y,sp_x,sp_y });
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
            //Debug.Log(current);
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
                        //Debug.Log(next_current);
                        col[0] = 0;
                        col[1] = info[current][1] + 1;
                        col[2] = Mathf.Abs(ep_x - res_x) + Mathf.Abs(ep_y - res_y);
                        col[3] = res_x;
                        col[4] = res_y;
                        col[5] = convert_current[0];
                        col[6] = convert_current[1];
                        info.Add(next_current, new List<int> { 0,info[current][1] + 1,Mathf.Abs(ep_x - res_x) + Mathf.Abs(ep_y - res_y),res_x,res_y,convert_current[0],convert_current[1] });
                        mid = Binary_insert(neighbors, new List<int> { 0, info[current][1] + 1, Mathf.Abs(ep_x - res_x) + Mathf.Abs(ep_y - res_y), res_x, res_y, convert_current[0], convert_current[1] });
                        neighbors.Insert(mid, new List<int> { 0, info[current][1] + 1, Mathf.Abs(ep_x - res_x) + Mathf.Abs(ep_y - res_y), res_x, res_y, convert_current[0], convert_current[1] });
                    }
                    else if (info.TryGetValue(next_current, out empty_col) && info[next_current][1] > info[current][1] + 1)//not empty and shorter way
                    {
                        //Debug.Log(next_current);
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
                return result_route;
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
            //Debug.Log("next");
        }

        //route
        result_route.Insert(0, convert_current);
        while (convert_current[0] != sp_x || convert_current[1] != sp_y)
        {
            res_x = info[current][5];
            res_y = info[current][6];
            current = res_x + "," + res_y;
            convert_current = StrConvert(current);
            result_route.Insert(0, convert_current);
        }

        return result_route;
    }

    void Start()
    {
        int[] input_coordinate = new int[4];
        route_idx = 0;
        //draw_idx = 0;

        input_coordinate[0] = 1;
        input_coordinate[1] = 1;
        input_coordinate[2] = 63;
        input_coordinate[3] = 63;

        binary_Cell = MazeMaker();

        route = A_star_pathfind(binary_Cell, input_coordinate);
    }

    void Update()
    {
        Vector3 temp;
        if(route_idx<route.Count)
        {
            if(Mathf.Round((32 - route[route_idx][0] % 65) * 0.5f * 10) > Mathf.Round(robot.transform.position.x*10))//go left
            {
                temp = new Vector3(0.2f, 0, 0);
                robot.transform.position += temp * Time.deltaTime * robot_speed;
            }
            else if(Mathf.Round((32 - route[route_idx][0] % 65) * 0.5f * 10) < Mathf.Round(robot.transform.position.x * 10))//go right
            {
                temp = new Vector3(-0.2f, 0, 0);
                robot.transform.position += temp * Time.deltaTime * robot_speed;
            }
            else if(Mathf.Round((32 - route[route_idx][1] % 65) * 0.5f * 10) > Mathf.Round(robot.transform.position.z * 10))//go up
            {
                temp = new Vector3(0, 0, 0.2f);
                robot.transform.position += temp * Time.deltaTime * robot_speed;
            }
            else if(Mathf.Round((32 - route[route_idx][1] % 65) * 0.5f * 10) < Mathf.Round(robot.transform.position.z * 10))//go down
            {
                temp = new Vector3(0, 0, -0.2f);
                robot.transform.position += temp * Time.deltaTime * robot_speed;
            }
            else
            {
                route_idx++;
                //Debug.Log(route_idx);
            }
        }/*
        if(draw_idx>=route_idx && route_idx<route.Count)
        {
            temp = new Vector3((32 - route[route_idx][0] % 65) * 0.5f, 0, (32 - route[route_idx][1] % 65) * 0.5f);
            Instantiate(route_obj, coordinate.position + temp, coordinate.rotation);
            route_idx++;
        }
        else if(draw_idx<route_idx && route_idx<route.Count)
        {
            draw_idx += Time.deltaTime * robot_speed;
        }*/
    }
}
