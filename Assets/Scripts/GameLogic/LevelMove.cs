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
    private GameObject player;

    private bool loadingMap = false;
    private ObjectPool ObjectPoolInstance;
    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        playerObject = player.GetComponent<PlayerObject>();

        ObjectPoolInstance = ObjectPool.SharedInstance;


        if (SceneManager.GetActiveScene().name == "Docks")
        {
            docksManager = FindObjectOfType<DocksManager>();
        }
        else if (SceneManager.GetActiveScene().name == "StartingRoom")
        {
            startingRoomManager = FindObjectOfType<StartingRoomManager>();
        }
        else if (SceneManager.GetActiveScene().name == "Estates")
        {
            estatesManager = FindObjectOfType<EstatesManager>();
        }
        else if (SceneManager.GetActiveScene().name == "Park")
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

            if (loadingMap)
            {
            }
            else
            {
                // Switch statement to determine current scene 
                switch (SceneManager.GetActiveScene().name)
                {
                    // Starting Room Level Move Logic
                    case "StartingRoom":
                        if (other.gameObject.name == "FrontDoor")
                        {
                            SceneManager.LoadScene("Estates", LoadSceneMode.Single);
                            SaveState();
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
                                        SaveState();
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
                                        SaveState();
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
                                        SaveState();
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
                                SaveState();
                                break;
                            case "ShoppingCenter_West":
                                GameObject westTextBox = GameObject.Find("WestTextBox");
                                if(westTextBox != null)
                                {
                                    String tmp = westTextBox.GetComponent<TextMeshPro>().text;
                                    if(tmp != null)
                                    {
                                        SceneManager.LoadScene(tmp, LoadSceneMode.Single);
                                        SaveState();
                                    }
                                }
                                break;
                            case "ShoppingCenter_South":
                                GameObject southTextBox = GameObject.Find("SouthTextBox");
                                if (southTextBox != null)
                                {
                                    String tmp = southTextBox.GetComponent<TextMeshPro>().text;
                                    if (tmp != null)
                                    {
                                        SceneManager.LoadScene(tmp, LoadSceneMode.Single);
                                        SaveState();
                                    }
                                }
                                break;
                            case "ShoppingCenter_East":
                                GameObject eastTextBox = GameObject.Find("EastTextBox");
                                if (eastTextBox != null)
                                {
                                    String tmp = eastTextBox.GetComponent<TextMeshPro>().text;
                                    if (tmp != null)
                                    {
                                        SceneManager.LoadScene(tmp, LoadSceneMode.Single);
                                        SaveState();
                                    }
                                }
                                break;
                        }
                        break;
                    case "ShoppingCenterInside":
                        if (other.gameObject.name == "Exit")
                        {
                            SceneManager.LoadScene("ShoppingCenter", LoadSceneMode.Single);
                            SaveState();
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
                                        SaveState();
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
                                        SaveState();
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
                                        SaveState();
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
                                        SaveState();
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
                                        SaveState();
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
                                        SaveState();
                                    }
                                }
                                break;
                        }
                        break;
                }

            }
        }
    }

    /* Saves State to Static reference */
    private void SaveState()
    {
        // Deactive all pooled objects
        ObjectPoolInstance.DeactivatePooledObjectEnemy();
        ObjectPoolInstance.DeactivatePooledObjectBullet();

        // Save player health and stamina
        PlayerPrefs.SetInt("Health", player.GetComponent<Health>().currHealth);
        PlayerPrefs.SetInt("Stamina", player.GetComponent<PlayerStamina>().currStamina);
        PlayerPrefs.Save();

        loadingMap = true;

        // Loop through each inventory slot, and assigns the object within to static reference
        foreach (GameObject Slot in playerObject.Inventory.InventorySlotList)
        {
            // No item in current slot: skip
            if (Slot.GetComponent<SlotItem>().GetItemInSlot() == null)
            {
                continue;
            }
            // Weapon in slot, add to static weapon reference
            else if (Slot.GetComponent<SlotItem>().GetItemInSlot() is Weapon weapon)
            {
                Weapon weaponCopy = new();
                weaponCopy.ReadWeapon(weapon);
                Slot.GetComponent<SlotItem>().RemoveItemFromSlot();
                PlayerInventory.StaticInventoryWeapon.Add(weaponCopy);
            }
            // Consumable item in slot, add to static consumable item reference
            else if (Slot.GetComponent<SlotItem>().GetItemInSlot() is ConsumableItem item)
            {
                ConsumableItem itemCopy = new();
                itemCopy.Clone(item);
                Slot.GetComponent<SlotItem>().RemoveItemFromSlot();
                PlayerInventory.StaticInventoryConsumable.Add(itemCopy);
            }
        }
    }

}