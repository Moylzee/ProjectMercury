using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    private void Start()
    {
       
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            // Check which Level to Move to based on which collider is interacted with
            
            // Estates Map
            case "Estates_North":
                SceneManager.LoadScene(FindObjectOfType<EstatesManager>().northTextBox.GetComponentInChildren<TextMeshPro>().text, LoadSceneMode.Single);
                break;
            case "Estates_East":
                SceneManager.LoadScene(FindObjectOfType<EstatesManager>().eastTextBox.GetComponentInChildren<TextMeshPro>().text, LoadSceneMode.Single);
                break;
            case "Estates_South":
                SceneManager.LoadScene(FindObjectOfType<EstatesManager>().southTextBox.GetComponentInChildren<TextMeshPro>().text, LoadSceneMode.Single);
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