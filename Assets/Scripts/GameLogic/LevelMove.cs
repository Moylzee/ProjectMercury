using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    private EstatesManager estatesManager;

    private void Start()
    {
        estatesManager = FindObjectOfType<EstatesManager>(); // Find the EstatesManager in the scene
        if (estatesManager == null)
        {
            Debug.LogError("EstatesManager not found in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            // Check which Level to Move to based on which collider is interacted with

            // Estates Map
            case "Estates_North":
                SceneManager.LoadScene(estatesManager.northTextBox.text, LoadSceneMode.Single);
                break;
            case "Estates_East":
                SceneManager.LoadScene(estatesManager.eastTextBox.text, LoadSceneMode.Single);
                break;
            case "Estates_South":
                SceneManager.LoadScene(estatesManager.southTextBox.text, LoadSceneMode.Single);
                break;
            case "House":
                SceneManager.LoadScene("House", LoadSceneMode.Single);
                break;
            case "Shop_Door":
                SceneManager.LoadScene("Shop", LoadSceneMode.Single);
                break;
            case "Estates_Building_Exit":
                SceneManager.LoadScene("Estates", LoadSceneMode.Single);
                break;

            // Docks Map




            default:
                Debug.Log("Unhandled tag: " + other.tag);
                break;
        }
    }
}