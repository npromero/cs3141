/*
 * Colton Eberlin
 * mainMenu is simply functions to call other program files to update the game
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    /*public void playMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void rulesMenu()
    {
        sceneManager.LoadScene(sceneManager.GetActiveScene().buildIndex + 1);
    }*/

    public void ButtonMoveScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    
    // Sets difficulty of game when moving to game scene and loads gmaeplay scene
    public void difficulty(int difficultyVal)
    {
        gameManager.setDifficulty = difficultyVal;
    }
    
    
    // Exits game
    public void quitGame()
    {
        Debug.Log("EXITED GAME SUCCESSFULLY");
        Application.Quit();
    }
    
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
