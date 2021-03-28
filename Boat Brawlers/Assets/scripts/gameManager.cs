/**
 * Niklas Romero
 *Battle brawllers gameManager
 *The class will handle the majority of static variables used in other classes.
 *The main variables are the current game stage, the current round and whos turn it is.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    /*
     * gameStage didctaes what actions are avaible.
     * gameStage = 0 means the game is in the placement stage
     * gameStage = 1 means the game is in the combat stage
     * gameStage = 3 means that player 1 has won
     * gameStage = 4 means that player 2 has won
     */
    public static int gameStage = 1;
    /*
     * The round is a variable for the end game screen to count how many rounds the game lasted.
     */
    public static int round = 1;
    /*
     * The current turn variable is a variable to dictate who can fire at the current moment
     * currentTurn = 1 means that player 1 can fire at a ship
     * currentTurn = 2 means that player 2 can fire at a ship
     */
    public static int currentTurn = 1;

    /*
     * Colton Eberlin
     * setDifficulty changes based on difficulty buttons to determine a difficulty
     * setDifficulty = 0 means easy
     * setDifficulty = 1 means normal/medium
     * setDifficulty = 2 means hard
     */
    public static int setDifficulty = 0;
}
