using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    public string difficulty;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Choose the difficulty for the AI
     * 
     * @param string difficulty     The difficutly level of the AI (e.g. "easy", "medium", "hard")
     */
    void selectDifficulty(string difficulty)
    {
        this.difficulty = difficulty;
    }

    /**
     * Choose which cell the AI will fire an attack onto
     */
    void chooseAttack()
    {
        // @todo
    }

    /**
     * Instantiate the AI grid and decide the placement of its ships
     */
    void establishGrid()
    {
        // @todo
    }
}
