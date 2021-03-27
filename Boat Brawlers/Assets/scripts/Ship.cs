using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    /**
     * Struct to maintain the coordinate position of a ship
     */
    private struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    // Main ship variables
    public int length { get; }
    private Position frontPosition;                         // The coordinates of the front of the ship

    public int x { 
        get
        {
            return frontPosition.X;
        }
    }
    public int y
    {
        get
        {
            return frontPosition.Y;
        }
    }

    //public Vector2 frontPosition;

    private int orientation { get; set; }           // What direction the ship is facing
    private bool[] damagedSections;                 // Which locations have been damaged (True if section has been damaged, false otherwise)

    public GameObject ship;
    public bool repositionAllowed;                  // Used to keep track of whether or not the game allows for the ships to be repositioned

    /**
     * Constructor
     * 
     * @param int shipLength        The length of the ship
     * @param int xLocation         X coordinate of the front of the ship
     * @param int yLocation         Y coordinate of the front of the ship
     * @param int orientation       The direction the ship is facing (0=North, 90=East, 180=South, 270=West)
     */
    public Ship(int shipLength, int xLocation, int yLocation, int orientation)
    {
        length = shipLength;
        frontPosition.X = xLocation;
        frontPosition.Y = yLocation;
        this.orientation = orientation;
        damagedSections = new bool[length];
        repositionAllowed = true;

        // Start the ship with no damage taken
        for (int i = 0; i < length; i++)
        {
            damagedSections[i] = false;
        }
    }

    /**
     * Attack a ship at a specific location
     * 
     * @param xPos  x position being attacked
     * @param yPos  y position being attacked
     * @return int  The length of the ship if it was sunk, -1 otherwise
     */
    public int Attack(int xPos, int yPos)
    {
        // Check if this attack is on a section of the ship
        int damageLocationIdx = positionOnShip(xPos, yPos);
        if (damageLocationIdx >= 0)
        {
            damagedSections[damageLocationIdx] = true;
        }

        for (int i = 0; i < length; i++)
        {
            if (!(damagedSections[i]))
            {
                return -1;
            }
        }

        return length;
    }

    /**
     * Helper method to check if a coordinate is a part of a ship
     * 
     * @param xPos      The x coord to check
     * @param yPos      The y coord to check
     * @return int      The index of the position on the ship, or -1 if the coordinate is not on the ship
     */
    private int positionOnShip(int xPos, int yPos)
    {
        List<Position> positions = getShipPositions();
        Position positionToFind = new Position(xPos, yPos);

        return positions.IndexOf(positionToFind);
    }

    private List<Position> getShipPositions()
    {
        // Depending on the direction, we need to track how much to
        // increase or decrease the x and y values while travsersing the ship
        int xDir = 0;
        int yDir = 0;
        List <Position> positions = new List<Position>();

        switch (orientation)
        {
            case 0:
                // Ship is vertical; get y coordinates from top to bottom, x coordinate stays the same
                xDir = 0;
                yDir = 1;
                break;
            case 180:
                // Ship is vertical; get y coordinates from bottom to top, x coordinate stays the same
                xDir = 0;
                yDir = -1;
                break;
            case 90:
                // Ship is horizontal; get x coordinates from right to left, y coordinate stays the same
                xDir = -1;
                yDir = 0;
                break;
            case 270:
                // Ship is horizontal; get x coordinates from left to right, y coordinate stays the same
                xDir = 1;
                yDir = 0;
                break;
        }

        int tempX = frontPosition.X;
        int tempY = frontPosition.Y;
        for (int i = 0; i < length; i++)
        {
            positions.Insert(i, new Position { X = (tempX + xDir), Y = (tempY + yDir) });
        }

        return positions;
    }

    public bool isDragging;

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;

        // @todo
        // Lock the ship to the closest tile
        transform.position = new Vector3((int) transform.position.x, (int) transform.position.y, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, -1);
        ship = (GameObject)Instantiate(ship, transform);
    }


    // Update is called once per frame
    void Update()
    {
        if (repositionAllowed)
        {
            if (isDragging)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(transform.position.x, transform.position.y, 0);
                frontPosition.X = (int) mousePosition.x;
                frontPosition.Y = (int) mousePosition.y;
                transform.Translate(mousePosition);
            }

            if (Input.GetKeyDown("r"))
            {
                transform.Rotate(0, 0, 90);
            }
        }
    }
}
