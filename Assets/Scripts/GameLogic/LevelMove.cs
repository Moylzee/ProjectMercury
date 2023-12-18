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
                SaveInventoryToStatic();
                SceneManager.LoadScene("Estates", LoadSceneMode.Single);
            }
        }
        else
        {
            return;
        }
    }


    private void SaveInventoryToStatic()
    {
        // reference to the player inventory
        var playerInventory = GameObject.FindWithTag("Player").GetComponent<PlayerObject>().Inventory;

       //TODO: Implement a save to static ref from the player inventory

        foreach(GameObject slot in playerInventory.InventorySlotList)
        {
            SlotItem slotRef = slot.GetComponentInChildren<SlotItem>();
            if(slotRef.GetItemInSlot() is Weapon weapon)
            {   
                PlayerInventory.StaticInventoryWeapon.Add(weapon);
            }
            else if(slotRef.GetItemInSlot() is ConsumableItem consumable)
            {
                PlayerInventory.StaticInventoryConsumable.Add(consumable);
            }
        }


    }
}
