using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIPlayer : MonoBehaviour
{

    public GameObject player1;
    //private grid grid;
    private GameObject[,] map;

    public string difficulty;
    public double difficultyPercent;
    
    // 2d array for the gird information; 1=unchecked; 0=miss; 2=hit
    private int[,] gridData;
    private int gridSize;
    // 2d array showing posibility of a peice being there,
    // for unchecked spots it is the sum of cardinally adjacent spots in gridData
    // where out of bounds spots count as zero, for misses and hits is -1
    private int[,] gridProbability;

    // Start is called before the first frame update
    void Start()
    {
        selectDifficulty("e");
        setup();
        // TEST CODE
        //Tuple<int,int> coords = randomAttackLocation();
    }
    
    //Test code
    int hitOnEven = 0;
    void OnMouseDown()
    {
        hitOnEven++;
        SpriteRenderer tileRenderer = GetComponent<SpriteRenderer>();
        if (hitOnEven % 2 == 0)
        {
            tileRenderer.color = new Color(1, 0, 0, 1);
        }
        else
        {
            tileRenderer.color = new Color(1, 1, 1, 1);
        }
    }
    //end Test code

    bool firstTime = true;
    // Update is called once per frame
    void Update()
    {
        if (firstTime==true)
        {
            firstTime = false;
            map = player1.GetComponent<grid>().getGrid();
        }

        // Test code should run on turn in final product
        if (Input.GetMouseButtonDown(1))
        {
            chooseAttack();
        }
    }

    /**
     * Choose the difficulty for the AI
     * 
     * @param string difficulty     The difficutly level of the AI (e.g. "easy", "medium", "hard")
     */
    void selectDifficulty(string difficulty)
    {
        this.difficulty = difficulty;
        if (String.Compare("easy", difficulty, StringComparison.OrdinalIgnoreCase) == 0)
        {
            difficultyPercent = 0.1;
        }
        else if (String.Compare("medium", difficulty, StringComparison.OrdinalIgnoreCase) == 0)
        {
            difficultyPercent = 0.5;
        }
        else if (String.Compare("hard", difficulty, StringComparison.OrdinalIgnoreCase) == 0)
        {
            difficultyPercent = 0.9;
        }
        else
        {
            difficultyPercent = 1.0;
        }
    }

    void setup()
    {
        gridData = new int[,] { 
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } };

        gridProbability  = new int[,] { 
            { 2, 3, 3, 3, 3, 3, 3, 3, 3, 2 }, 
            { 3, 4, 4, 4, 4, 4, 4, 4, 4, 3 }, 
            { 3, 4, 4, 4, 4, 4, 4, 4, 4, 3 }, 
            { 3, 4, 4, 4, 4, 4, 4, 4, 4, 3 }, 
            { 3, 4, 4, 4, 4, 4, 4, 4, 4, 3 }, 
            { 3, 4, 4, 4, 4, 4, 4, 4, 4, 3 }, 
            { 3, 4, 4, 4, 4, 4, 4, 4, 4, 3 }, 
            { 3, 4, 4, 4, 4, 4, 4, 4, 4, 3 }, 
            { 3, 4, 4, 4, 4, 4, 4, 4, 4, 3 }, 
            { 2, 3, 3, 3, 3, 3, 3, 3, 3, 2 } };
        gridSize = 10;
    }

    void updateProb( int x, int y)
    {
        if (x > -1 && x < gridSize && y > -1 && y < gridSize && gridProbability[x, y] != -1)
        {
            int sum = 0;
            if (x != 0)
            {
                sum += gridData[x - 1, y];
                if (x > 1 && gridData[x - 2, y]==2)
                {
                    sum++;
                }
            }
            if (x != (gridSize - 1))
            {
                sum += gridData[x + 1, y];
                if (x < (gridSize-2) && gridData[x + 2, y] == 2)
                {
                    sum++;
                }
            }
            if (y != 0)
            {
                sum += gridData[x, y - 1];
                if (y > 1 && gridData[x, y-2] == 2)
                {
                    sum++;
                }
            }
            if (y != (gridSize - 1))
            {
                sum += gridData[x, y + 1];
                if (y < (gridSize - 2) && gridData[x, y + 2] == 2)
                {
                    sum++;
                }
            }
            gridProbability[x, y] = sum;
        }
    }

    /**
     * Selects attack location at random
     * 
     * @param int gridSize    The width and height of the grid.
     * @return    A pair of integers x and y that represent the coordinantes to fire upon, where 0 <= x < girdSize & 0 <= y < girdSize
     */
    Tuple<int, int> randomAttackLocation()
    {
        System.Random rnd = new System.Random();
        int x =-1;
        int y =-1;
        int count = 0;
        bool found = true;
        do
        {
            if (count>500)
            {
                found = false;
                break;
            }
            count++;
            x = rnd.Next(0,gridSize);
            y = rnd.Next(0, gridSize);
        } while (gridData[x,y] != 1);

        // if it fails to generate a valid spot to fire at, choose the first valid spot 
        if (false == found)
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (gridData[i, j] == 1)
                    {
                        return Tuple.Create(i, j);
                    }
                }
            }
            // grid is full should not happen
        }
        // return the valid randomly generated location
        return Tuple.Create(x, y);
    }

    Tuple<int, int> smartAttackLocation()
    {
        int probMax = -2;
        int xMax = 0;
        int yMax = 0;
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (probMax < gridProbability[i, j])
                {
                    probMax = gridProbability[i, j];
                    xMax = i;
                    yMax = j;
                }
            }
        }
        return Tuple.Create(xMax, yMax);
    }

    /**
     * Choose which cell the AI will fire an attack onto
     */
    void chooseAttack()
    {
        // find location to fire at
        System.Random rnd = new System.Random();
        Tuple<int, int> location;
        if (rnd.NextDouble() > difficultyPercent)
        {
            location = randomAttackLocation();
            Debug.Log("random\n"); //Test code
        }
        else
        {
            location = smartAttackLocation();
            Debug.Log("smart\n"); //Test code
        }

        bool hit;
        // fire at location
        // In future hits will use acctual data.
        if (hitOnEven % 2 == 0)
        { 
            hit = true; 
        }
        else
        {
            hit = false;
        }
        //Update gridData and gridProbibility
        if (hit)
        {
            gridData[location.Item1, location.Item2] = 2;
            map[location.Item1, location.Item2].GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1); //Test code
        }
        else
        {
            gridData[location.Item1, location.Item2] = 0;
            map[location.Item1, location.Item2].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1); //Test code
        }
        gridProbability[location.Item1, location.Item2] = -1;
        updateProb(location.Item1-1, location.Item2);
        updateProb(location.Item1+1, location.Item2);
        updateProb(location.Item1, location.Item2-1);
        updateProb(location.Item1, location.Item2+1);
    }

    /**
     * Instantiate the AI grid and decide the placement of its ships
     */
    void establishGrid()
    {
        // @todo
    }


}
