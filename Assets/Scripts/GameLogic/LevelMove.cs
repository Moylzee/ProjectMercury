using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    private DocksManager docksManager;
    private StartingRoomManager startingRoomManager;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Docks")
        {
            docksManager = FindObjectOfType<DocksManager>();
        } else if (SceneManager.GetActiveScene().name == "StartingRoom") 
        {
            startingRoomManager = FindObjectOfType<StartingRoomManager>(); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (SceneManager.GetActiveScene().name == "Docks")
        {
            switch (other.tag)
            {
                // Check which Level to Move to based on which collider is interacted with

                // Docks Map

                default:
                    Debug.Log("Unhandled tag: " + other.tag);
                    break;
            }
        }
        else if (SceneManager.GetActiveScene().name == "StartingRoom")
        {
            if (GameObject.FindGameObjectWithTag("FrontDoor"))
            {
                SceneManager.LoadScene("Estates", LoadSceneMode.Single);
            }
        }
    }
}
