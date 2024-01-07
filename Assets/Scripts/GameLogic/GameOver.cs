using UnityEngine;
using UnityEngine.SceneManagement;

/* Script used to handle the Game over screen
* Once the player has died, this screen will display
* Methods are used in the Game over screen UI  
*/

public class GameOverScript : MonoBehaviour
{
    // Load the "Menu" scene by its name.
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    // Restart the game 
    public void RestartGame()
    {
        SceneManager.LoadScene("StartingRoom", LoadSceneMode.Single);
    }
    // Quit the Game 
    public void QuitGame()
    {
        Application.Quit();
    }
}