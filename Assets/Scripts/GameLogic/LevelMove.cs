using System.Collections;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
/ Script used to handle moving between levels (scenes)
/ Some scenes require dynamic loading of other scenes based on variable assigment of the level move objects 
/ This is handled in the switch cases when necessary 
*/
public class LevelMove : MonoBehaviour
{
    private DocksManager docksManager;
    private EstatesManager estatesManager;
    private StartingRoomManager startingRoomManager;
    private ParkManager parkManager;
    public static string previousScene;
    private ScenesManager sceneManager;

    private PlayerObject playerObject;

    private bool loadingMap = false;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerObject>();

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
    // Method used to keep track of the scene that the player is leaving 
    // Used to determine the spawn point in the next scene 
    private void UpdatePreviousScene()
    {
        previousScene = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check that the collision is with a level move object 
        // Prevents moving between levels when colliding with other objects 
        if (other.gameObject.tag == "LevelMove")
        {
        // Update Previous Scene
        UpdatePreviousScene();
        // Switch statement to determine current scene 
        switch (SceneManager.GetActiveScene().name)
        {
            // Starting Room Level Move Logic
            case "StartingRoom":
                if (other.gameObject.name == "FrontDoor")
                {
                    SceneManager.LoadScene("Estates", LoadSceneMode.Single);
                }
            break;
        if (loadingMap)
        {
            SaveInventory();
        }
        else
        {
            switch (SceneManager.GetActiveScene().name)
            {
                // Starting Room Level Move Logic
                case "StartingRoom":
                    if (other.gameObject.name == "FrontDoor")
                    {
                        SceneManager.LoadScene("Estates", LoadSceneMode.Single);
                        loadingMap = true;
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
                                    loadingMap = true;
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
                                    loadingMap = true;
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
                                    loadingMap = true;
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
                            loadingMap = true;
                            break;
                        case "Estates":
                            SceneManager.LoadScene("Estates", LoadSceneMode.Single);
                            loadingMap = true;
                            break;
                        case "CarPark":
                            SceneManager.LoadScene("CarPark", LoadSceneMode.Single);
                            loadingMap = true;
                            break;
                    }
                    break;
                case "ShoppingCenterInside":
                    if (other.gameObject.name == "Exit")
                    {
                        SceneManager.LoadScene("ShoppingCenter", LoadSceneMode.Single);
                        loadingMap = true;
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
                                    loadingMap = true;
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
                                    loadingMap = true;
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
                                    loadingMap = true;
                                }
                            }
                            break;
                    }
                    break;

            // Docks Level Move Logic
            case "Docks":
                switch (other.gameObject.name)
                {
                    // North Exit
                    case "Docks_North":
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
                    // West Exit 
                    case "Docks_West":
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
                    // East Exit
                    case "Docks_East":
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
            } 
        }
    }
}