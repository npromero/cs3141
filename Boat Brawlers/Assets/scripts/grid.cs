using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour
{
    public int columns,rows;
    public float tileSize;
    public GameObject tile;
    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int cols = 0; cols < columns; cols++)
            {
                GameObject tiles = (GameObject) Instantiate(tile, transform);
                float x = cols * tileSize;
                float y = row * -tileSize;
                tiles.transform.position = new Vector2(x, y);
            }
        }
        float gridW = columns * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);
    }
}
