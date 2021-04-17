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
    public int player;
    public float tileSize;
    public GameObject tileFile;
    public GameObject[,] gridMap;

	// Ships
	public GameObject destroyer;
	public GameObject carrier;
	public GameObject cruiser;
	public GameObject battleship;
	public GameObject submarine;

    // Start is called before the first frame update
    void Start()
    {
        gridMap = new GameObject[columns, rows];
        GenerateGrid();  

		// Spawn ships for player 1
		if (player == 1) {
			SpawnShips();
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject[,] getGrid()
    {
        return gridMap;
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
                float x = (cols * tileSize) + xStart;
                float y = (row * -tileSize) - yStart;
                tiles.transform.position = new Vector2(x, y);
                gridMap[cols, row] = tiles;
                tiles = null;
            }
        }
        float gridW = columns * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);
    }
	
	private void SpawnShips() {
		destroyer = (GameObject) Instantiate(destroyer, transform);
		destroyer.transform.Translate(new Vector2(-3, -13));
		destroyer.transform.Rotate(new Vector3(0, 0, 90));

		carrier = (GameObject) Instantiate(carrier, transform);
		carrier.transform.Translate(new Vector2(-7, -11));
		carrier.transform.Rotate(new Vector3(0, 0, 90));

		cruiser = (GameObject) Instantiate(cruiser, transform);
		cruiser.transform.Translate(new Vector2(-11, -12));

		battleship = (GameObject) Instantiate(battleship, transform);
		battleship.transform.Translate(new Vector2(-7, -13));
		battleship.transform.Rotate(new Vector3(0, 0, 90));

		submarine = (GameObject) Instantiate(submarine, transform);
		submarine.transform.Translate(new Vector2(-2, -11));
		submarine.transform.Rotate(new Vector3(0, 0, 90));
	}
   
}
