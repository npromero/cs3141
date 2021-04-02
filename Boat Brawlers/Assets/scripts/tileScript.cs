/**
 * Niklas Romero
 *Battle brawllers tile behavor
 * The class handles all tile functionalities
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileScript : MonoBehaviour
{
    public int player;
    public SpriteRenderer tileRenderer;
    // Start is called before the first frame update
    void Start()
    {
        tileRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }
    void OnMouseDown()
    {
        if(player == gameManager.currentTurn)
        {
            tileRenderer.color = new Color(1, 0, 0, 1);
            if(gameManager.currentTurn == 1)
            {
                gameManager.currentTurn = 2;
            }
            else 
            {
                gameManager.currentTurn = 1;
                gameManager.round = gameManager.round + 1;
            }
        }
        
    }
}
