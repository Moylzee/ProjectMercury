using System.Collections;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    private DocksManager docksManager;
    private EstatesManager estatesManager;
    private StartingRoomManager startingRoomManager;
    private ParkManager parkManager;
    public static string previousScene;

    private ScenesManager sceneManager;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Docks")
        {
            docksManager = FindObjectOfType<DocksManager>();
        } else if (SceneManager.GetActiveScene().name == "StartingRoom") 
        {
            startingRoomManager = FindObjectOfType<StartingRoomManager>(); 
        } else if (SceneManager.GetActiveScene().name == "Estates")
        {
            estatesManager = FindObjectOfType<EstatesManager>();
        } else if (SceneManager.GetActiveScene().name == "Park")
        {
            parkManager = FindObjectOfType<ParkManager>();
        } 
    }
    
    private void UpdatePreviousScene()
    {
        previousScene = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        UpdatePreviousScene();
        switch (SceneManager.GetActiveScene().name)
        {
            // Starting Room Level Move Logic
            case "StartingRoom":
                if (other.gameObject.name == "FrontDoor")
                {
                    SceneManager.LoadScene("Estates", LoadSceneMode.Single);
                }
            break;


            // Estates Level Move Logic 
            case "Estates":
                switch (other.gameObject.name)
                {
                    // North Exit
                    case "Estates_North":
                        GameObject northTextBox = GameObject.Find("NorthTextBox");
                        if (northTextBox != null)
                        {
                            String tmp = northTextBox.GetComponent<TextMeshPro>().text;
                            if (tmp != null)
                            {
                                SceneManager.LoadScene(tmp, LoadSceneMode.Single);
                            }
                        }
                    break;
                    // South Exit
                    case "Estates_South":
                        GameObject southTextBox = GameObject.Find("SouthTextBox");
                        if (southTextBox != null)
                        {
                            String tmp = southTextBox.GetComponent<TextMeshPro>().text;
                            if (tmp != null)
                            {
                                SceneManager.LoadScene(tmp, LoadSceneMode.Single);
                            }
                        }
                    break;
                    // East Exit
                    case "Estates_East":
                        GameObject eastTextBox = GameObject.Find("EastTextBox");
                        if (eastTextBox != null)
                        {
                            String tmp = eastTextBox.GetComponent<TextMeshPro>().text;
                            if (tmp != null)
                            {
                                SceneManager.LoadScene(tmp, LoadSceneMode.Single);
                            }
                        }
                    break;
                }
            break;

            // Shopping Center Level Move Logic
            case "ShoppingCenter":
                switch (other.gameObject.name)
                {
                    case "Entrance":
                        SceneManager.LoadScene("ShoppingCenterInside", LoadSceneMode.Single);
                    break;
                    case "Estates":
                        SceneManager.LoadScene("Estates", LoadSceneMode.Single);
                    break;
                    case "CarPark":
                        SceneManager.LoadScene("CarPark", LoadSceneMode.Single);
                    break;
                }
            break;
            case "ShoppingCenterInside":
                if (other.gameObject.name == "Exit")
                {
                    SceneManager.LoadScene("ShoppingCenter", LoadSceneMode.Single);
                }
            break;


            // Park Level Move Logic
            case "Park":
                switch (other.gameObject.name)
                {
                    // West Exit
                    case "Park_West":
                        GameObject westTextBox = GameObject.Find("WestTextBox");
                        if (westTextBox != null)
                        {
                            String tmp = westTextBox.GetComponent<TextMeshPro>().text;
                            if (tmp != null)
                            {
                                SceneManager.LoadScene(tmp, LoadSceneMode.Single);
                            }
                        }
                    break;
                    // South Exit
                    case "Park_South":
                        GameObject southTextBox = GameObject.Find("SouthTextBox");
                        if (southTextBox != null)
                        {
                            String tmp = southTextBox.GetComponent<TextMeshPro>().text;
                            if (tmp != null)
                            {
                                SceneManager.LoadScene(tmp, LoadSceneMode.Single);
                            }
                        }
                    break;
                    // East Exit
                    case "Park_East":
                        GameObject eastTextBox = GameObject.Find("EastTextBox");
                        if (eastTextBox != null)
                        {
                            String tmp = eastTextBox.GetComponent<TextMeshPro>().text;
                            if (tmp != null)
                            {
                                SceneManager.LoadScene(tmp, LoadSceneMode.Single);
                            }
                        }
                    break;
                }
            break;

            // Docks Level Move Logic
            case "Docks":
                switch (other.gameObject.name)
                {
                    case "Estates":
                        SceneManager.LoadScene("Estates", LoadSceneMode.Single);
                    break;
                    case "Park":
                        SceneManager.LoadScene("Park", LoadSceneMode.Single);
                    break;
                    case "ShoppingCenter":
                        SceneManager.LoadScene("ShoppingCenter", LoadSceneMode.Single);
                    break;
                    default:
                        Debug.Log("Unhandled Name: " + other.gameObject.name);
                    break;
                }
            break;
            default:
                Debug.Log("Unhandled Scene: " + SceneManager.GetActiveScene().name);
            break;
        } 
    }
}