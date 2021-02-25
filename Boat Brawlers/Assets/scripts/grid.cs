/**
 * Niklas Romero
 *Battle brawllers grid creater
 * The class makes a grid of tiles with uniform spacing 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour
{
    public int xStart, yStart;
    public int columns,rows;
    public float tileSize;
    public GameObject tileFile;
    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
     *The method creates the grid of tiles with specified starting cordinates, size of grid and the size of each tile
     */
    private void GenerateGrid()
    {
        //nested for loop making the tiles as seperate objects in a pattern using the tile size in the calculation.
        for (int row = 0; row < rows; row++)
        {
            for (int cols = 0; cols < columns; cols++)
            {
                GameObject tiles =  (GameObject) Instantiate(tileFile, transform);
                float x = xStart + cols * tileSize;
                float y = yStart + row * -tileSize;
                tiles.transform.position = new Vector2(x, y);
                tiles = null;
            }
        }
        float gridW = columns * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);
    }
   
}
