using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
/ Script for the pause menu in the game 
/ Methods use are for checking the status of the game (Game over, Game paused, etc)
/ Displays the relevant UI based on status of the game 
*/

public class PauseMenu : MonoBehaviour
{
    // Boolean to track if game is paused 
    public static bool GamePaused = false;
    // Variable for the pause menu UI
    public GameObject pauseUI;

    void Update()
    {
        // Enable/Disable Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    // Method to resume the game
    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }
    // Method to pause the game 
    void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    // Method the Quit the Game 
    public void QuitGame()
    {
        Application.Quit();
    }
}