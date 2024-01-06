using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
* Script to control the Main Menu of the game.
* Functions: PlayGame() and QuitGame().
* PlayGame() loads the StartingRoom Scene.
* QuitGame() quits the game.
*/
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("StartingRoom", LoadSceneMode.Single);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}