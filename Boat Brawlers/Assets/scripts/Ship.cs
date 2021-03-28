using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    // Main ship variables
    public int length { get; }

    private int orientation { get; set; }           // What direction the ship is facing
    private bool[] damagedSections;                 // Which locations have been damaged (True if section has been damaged, false otherwise)

    public GameObject ship;

    /**
     * Constructor
     * 
     * @param int shipLength        The length of the ship
     * @param int xLocation         X coordinate of the front of the ship
     * @param int yLocation         Y coordinate of the front of the ship
     * @param int orientation       The direction the ship is facing (0=North, 90=East, 180=South, 270=West)
     */
    public Ship(int shipLength, int orientation)
    {
        length = shipLength;
        this.orientation = orientation;
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
        List<Vector2> positions = getShipPositions();
        Vector2 positionToFind = new Vector2(xPos, yPos);

        return positions.IndexOf(positionToFind);
    }

    private List<Vector2> getShipPositions()
    {
        // Depending on the direction, we need to track how much to
        // increase or decrease the x and y values while travsersing the ship
        Vector2 facingDir = new Vector2(0, 0);
        List <Vector2> positions = new List<Vector2>();

        switch (orientation)
        {
            case 0:
                // Ship is vertical; get y coordinates from top to bottom, x coordinate stays the same
                facingDir.x = 0;
                facingDir.y = 1;
                break;
            case 180:
                // Ship is vertical; get y coordinates from bottom to top, x coordinate stays the same
                facingDir.x = 0;
                facingDir.y = -1;
                break;
            case 90:
                // Ship is horizontal; get x coordinates from right to left, y coordinate stays the same
                facingDir.x = -1;
                facingDir.y = 0;
                break;
            case 270:
                // Ship is horizontal; get x coordinates from left to right, y coordinate stays the same
                facingDir.x = 1;
                facingDir.y = 0;
                break;
        }

        Vector3 pos = transform.position;
        for (int i = 0; i < length; i++)
        {
            positions.Insert(i, new Vector2((int)pos.x + facingDir.x, (int)pos.y + facingDir.y));
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
    }

    void OnCollisionStay(Collision collide)
    {
        Debug.Log("Collided with" + collide.gameObject.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        isDragging = false;
        ship = (GameObject)Instantiate(ship, transform);
    }


    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameStage == 0)
        {
            if (isDragging)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(transform.position.x, transform.position.y, 0);
                Vector3 shipPos = transform.position;
                shipPos.x = (int) mousePosition.x;
                shipPos.y = (int) mousePosition.y;
                transform.Translate(new Vector2((int) shipPos.x, (int) shipPos.y));

                if (Input.GetKeyDown("r"))
                {
                    transform.Rotate(0, 0, 90);
                }
            }

            // Make sure the ship is exactly vertical or horizontal
            if (transform.rotation.eulerAngles.z % 90 != 0)
            {
                // Snap the value back to being exactly horizontal or vertical
                transform.rotation = Quaternion.Euler(0, 0, (int) (transform.rotation.eulerAngles.z / 90) * 90);
            }
        }
    }
}
