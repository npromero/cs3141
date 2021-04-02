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
        selectDifficulty("impossible",10);
    }

    bool firstTime = true;
    // Update is called once per frame
    void Update()
    {
        if (firstTime==true)
        {
            firstTime = false;
            map = player1.GetComponent<grid>().getGrid();
        }
    }

    /**
     * Choose the difficulty for the AI
     * 
     * @param string difficulty     The difficutly level of the AI (e.g. "easy", "medium", "hard")
     * @param int gridSize     The size of the grid
     */
    public void selectDifficulty(string difficulty, int gridSize)
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
        setup(gridSize);
    }

    void setup(int size)
    {
        gridData = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                gridData[i, j] = 1;
            }
        }

        gridProbability = new int[size, size];
        gridProbability[0, 0] = 2;
        gridProbability[(size - 1), 0] = 2;
        gridProbability[0, (size - 1)] = 2;
        gridProbability[(size - 1), (size - 1)] = 2;
        for (int i=1; i<size-1; i++)
        {
            gridProbability[i, 0] = 3;
            gridProbability[i, size - 1] = 3;
            gridProbability[0, i] = 3;
            gridProbability[size - 1, i] = 3;
        }
        for (int i = 1; i < size-1; i++)
        {
            for (int j = 1; j < size-1; j++)
            {
                gridProbability[i, j] = 4;
            }
        }
        gridSize = size;
    }

    void updateProb( int x, int y)
    {
        if (x > -1 && x < gridSize && y > -1 && y < gridSize && gridProbability[x, y] != -1)
        {
            int sum = 0;
            if (x != 0)
            {
                sum += gridData[x - 1, y];
                if (x > 1 && gridData[x - 1, y] == 2 && gridData[x - 2, y]==2)
                {
                    sum+=4;
                }
            }
            if (x != (gridSize - 1))
            {
                sum += gridData[x + 1, y];
                if (x < (gridSize-2) && gridData[x + 1, y] == 2 && gridData[x + 2, y] == 2)
                {
                    sum+=4;
                }
            }
            if (y != 0)
            {
                sum += gridData[x, y - 1];
                if (y > 1 && gridData[x, y -1] == 2 && gridData[x, y-2] == 2)
                {
                    sum+=4;
                }
            }
            if (y != (gridSize - 1))
            {
                sum += gridData[x, y + 1];
                if (y < (gridSize - 2) && gridData[x , y + 1] == 2 && gridData[x, y + 2] == 2)
                {
                    sum+=4;
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
        hit = false;
        //Update gridData and gridProbibility
        if (hit)
        {
            gridData[location.Item1, location.Item2] = 2;
            map[location.Item1, location.Item2].GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1); //Test code
        }
        else
        {
            gridData[location.Item1, location.Item2] = 0;
            map[location.Item1, location.Item2].GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1); //Test code
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
