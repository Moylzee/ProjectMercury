using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    private DocksManager docksManager;
    private StartingRoomManager startingRoomManager;

    private ScenesManager sceneManager;

    private void Start()
    {
        sceneManager = FindObjectOfType<ScenesManager>();
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
            if (other.gameObject == GameObject.FindGameObjectWithTag("FrontDoor"))
            {
                SceneManager.LoadScene("Estates", LoadSceneMode.Single);
            }
        }
        else
        {
            return;
        }
    }
}
