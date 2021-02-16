using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour
{
   
    public int columns,rows;
    public float tileSize;
    public GameObject tileFile;
    public GameObject[,] tileMap;
    // Start is called before the first frame update
    void Start()
    {
        tileMap = new GameObject[columns,rows];
        GenerateGrid();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;
            GameObject tile = tileMap[1,1];
            tile.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void GenerateGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int cols = 0; cols < columns; cols++)
            {
                GameObject tiles = (GameObject) Instantiate(tileFile, transform);
                float x = cols * tileSize;
                float y = row * -tileSize;
                tiles.transform.position = new Vector2(x, y);
                tileMap[cols,row] = tiles;
            }
        }
        float gridW = columns * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);
    }
   
}
