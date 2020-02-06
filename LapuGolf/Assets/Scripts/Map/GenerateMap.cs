using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public Object[] Blocks;
    public Object[] Collecatable;
    public Object[] RefreshPoint;
    public float step = 3;
    public float height = 1.5f;

    private int blocknum = 0;
    private GameObject MapObjects;
    int[,] MapData;
    GameObject[,] MapGo;

    public string filepath_map = "Assets/Resources/Data/map01.txt";
    public string filepath_collectable = "Assets/Resources/Data/map01_collectable.txt";
    public string filepath_refreshpoint = "Assets/Resources/Data/map01_refreshpoint.txt";
    int x, y = 0;

    // Start is called before the first frame update
    void Start()
    {

        MapObjects = GameObject.Find("MapObjects");
        
        DrawObject(filepath_map, Blocks);
        DrawObject(filepath_collectable, Collecatable);
        DrawObject(filepath_refreshpoint, RefreshPoint);



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DrawObject(string path, Object[] obj) 
    {
        ReadDataFromTXT(path);

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (MapData[i, j] > 0)
                {
                    MapGo[i, j] = Instantiate(obj[MapData[i, j] - 1], new Vector3(i * step, height, j * step), Quaternion.identity) as GameObject;
                    MapGo[i, j].transform.SetParent(MapObjects.transform);
                }
            }
        }
    }

    void ReadDataFromTXT(string path)
    {
        string[] reader = System.IO.File.ReadAllLines(path);
     
        x = reader.Length;
        y = reader[0].Length;
 
        MapData = new int[x, y];
        MapGo = new GameObject[x, y];

        for (int i = 0; i < x; i++)
        {
            string a = reader[i];
            for (int j = 0; j < y; j++)
            {
                MapData[i, j] = a[j] - 48;
               
            }
        }
    }


}


