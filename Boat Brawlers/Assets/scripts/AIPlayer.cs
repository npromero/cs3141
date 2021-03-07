using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIPlayer : MonoBehaviour
{
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
        selectDifficulty("hard");
        setup();
    }

    bool running = false;
    // Update is called once per frame
    void Update()
    {
        if (running == false)
        {
            Console.WriteLine("TEST");
            running = true;
            chooseAttack();
            running = false;
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
        if (x > -1 && x < 10 && y > -1 && y < 10)
        {
            int sum = 0;
            if (x != 0)
            {
                sum += gridData[x - 1, y];
            }
            if (x != 9)
            {
                sum += gridData[x + 1, y];
            }
            if (y != 0)
            {
                sum += gridData[x, y - 1];
            }
            if (y != 9)
            {
                sum += gridData[x, y + 1];
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
        }
        else
        {
            location = smartAttackLocation();
        }

        // fire at location
        bool hit;
        /*
        // temp code for testing
        Console.WriteLine(String.Format("({0},{1}) - Enter 1 for hit, anything else for miss:",location.Item1,location.Item2));
        int readChar = Console.Read();
        if (readChar == 49)
        {
            hit = true;
        }
        else
        {
            hit = false;
        }
        //end of temp code for testing
        */
        //Update gridData and gridProbibility
        if (hit)
        {
            gridData[location.Item1, location.Item2] = 2;
        }
        else
        {
            gridData[location.Item1, location.Item2] = 0;
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
