using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

    private int length;
    private int x;
    private int y;
    private bool isHorizontal;
    private bool[] spotIsHit;

    public Ship( int shipLength, int xLocation, int yLocation, bool placedHorizontally)
    {
        length = shipLength;
        x = xLocation;
        y = yLocation;
        isHorizontal = placedHorizontally;
        spotIsHit = new bool[length];
        for (int i = 0; i < length; i++)
        {
            spotIsHit[i] = false;
        }
    }

    // xPos = x position being attacked
    // yPos = y position being attacked
    // Function verifys the location hit the boat and marks the spot as hit if it is.
    // Returns if the ship is still alive
    public bool Attack( int xPos, int yPos )
    {
        if ((yPos - y) >= 0 && (yPos - y) < length && !(isHorizontal) && x == xPos)
        {
            spotIsHit[(yPos - y)] = true;
        }
        else if ((xPos - x) >= 0 && (xPos - x) < length && isHorizontal && y == yPos)
        {
            spotIsHit[(xPos - x)] = true;
        }
        else
        {
            //Possible Cheat Protection
        }

        for (int i = 0; i < length; i++)
        {
            if (!(spotIsHit[i]))
            {
                return true;
            }
        }
        return false;
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
