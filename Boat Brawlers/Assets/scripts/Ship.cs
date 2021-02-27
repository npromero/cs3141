using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    /**
     * Enum to keep track of a ship's orientation
     */
    private enum Orientation : int
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

    /**
     * Struct to maintain the coordinate position of a ship
     */
    private struct Position : int
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    // Main ship variables
    public int length { get; };
    private Position front;                         // The coordinates of the front of the ship
    public Orientation orientation { get; set; };   // What direction the ship is facing
    private bool[] damagedSections;                // Which locations have been damaged (True if section has been damaged, false otherwise)

    /**
     * Constructor
     * 
     * @param int shipLength        The length of the ship
     * @param int xLocation         X coordinate of the front of the ship
     * @param int yLocation         Y coordinate of the front of the ship
     * @param int orientation       The direction the ship is facing (0=North, 1=East, 2=South, 3=West)
     */
    public Ship(int shipLength, int xLocation, int yLocation, int orientation)
    {
        length = shipLength;
        front.X = xLocation;
        front.Y = yLocation;
        this.orientation = (Orientation)orientation;
        damagedSections = new bool[length];

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
     * @return      True if the ship is still alive, false otherwise
     */
    public bool Attack(int xPos, int yPos)
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
                return true;
            }
        }

        return false;
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
        return positions.IndexOf(position => position.X == xPos && position.Y == yPos);
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
            case North:
                // Ship is vertical; get y coordinates from top to bottom, x coordinate stays the same
                xDir = 0;
                yDir = 1;
                break;
            case South:
                // Ship is vertical; get y coordinates from bottom to top, x coordinate stays the same
                xDir = 0;
                yDir = -1;
                break;
            case East:
                // Ship is horizontal; get x coordinates from right to left, y coordinate stays the same
                xDir = -1;
                yDir = 0;
                break;
            case West:
                // Ship is horizontal; get x coordinates from left to right, y coordinate stays the same
                xDir = 1;
                yDir  0;
                break;
        }

        int tempX = front.X;
        int tempY = front.Y;
        for (int i = 0; i < length; i++)
        {
            positions.Insert(i, new Position { X = (tempX + xDir), Y = (tempY + yDir) });
        }

        return positions;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
