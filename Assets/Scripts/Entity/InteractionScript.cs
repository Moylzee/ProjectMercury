using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Interaction Script class, responsible */
public class InteractionScript : MonoBehaviour
{
    // Interact Prompt Object
    public GameObject InteractPrompt;

    // List of game objects within player
    private List<GameObject> ClosestItemToPlayer;
    private GameObject ClosestObject;

    private void Start()
    {
        // Sets the interaction prompt text to text in settings
        InteractPrompt.GetComponent<Text>().text = Settings.GetData().GetKey_InteractKey().ToUpper();
        ClosestItemToPlayer = new List<GameObject>();
    }

    /* Trigger Enter method, checks if item or lootbox is nearby */
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            ClosestItemToPlayer.Add(collision.gameObject);

            if (ClosestItemToPlayer.Count == 1)
            {
                // Enable Interact Prompt only if this is the first trigger
                InteractPrompt.SetActive(true);
            }
        }
        if (collision.gameObject.CompareTag("Lootbox"))
        {
            ClosestItemToPlayer.Add(collision.gameObject);
            if (ClosestItemToPlayer.Count == 1)
            {
                // Enable Interact Prompt only if this is the first trigger
                InteractPrompt.SetActive(true);
            }
        }

        FindClosestObject();

    }

    /* On Trigger Exit method, removes item or lootbox from nearby variable */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            ClosestItemToPlayer.Remove(collision.gameObject);

            if (GetComponentInParent<PlayerObject>().ItemNearby == collision.gameObject)
            {
                GetComponentInParent<PlayerObject>().ItemNearby = null;
            }

            FindClosestObject();
            if (ClosestItemToPlayer.Count <= 0)
            {
                // Disable Interact Prompt when no relevant triggers are left
                InteractPrompt.SetActive(false);
                GetComponentInParent<PlayerObject>().ItemNearby = null;
            }
        }
        else if (collision.gameObject.CompareTag("Lootbox"))
        {
            ClosestItemToPlayer.Remove(collision.gameObject);

            if (GetComponentInParent<PlayerObject>().BoxNearby == collision.gameObject)
            {
                GetComponentInParent<PlayerObject>().BoxNearby = null;
            }

            FindClosestObject();
            if (ClosestItemToPlayer.Count <= 0)
            {
                // Disable Interact Prompt when no relevant triggers are left
                InteractPrompt.SetActive(false);
                GetComponentInParent<PlayerObject>().BoxNearby = null;
            }
        }
    }

    /* Function to find closest object to player, by comparing distance. Time: O(n), Space: O(1) */
    private void FindClosestObject()
    {
        // If list is empty return
        if (ClosestItemToPlayer.Count <= 0)
        {
            return;
        }

        // Create temp gameobject, and large distance;
        GameObject temp = null;
        float distance = float.MaxValue;

        // Loop through every object in the list
        // Compare distance from player to object, and compare against prior shortest distance
        // If shorter, than replace, else continue
        foreach (GameObject obj in ClosestItemToPlayer)
        {
            float dist = (Vector2.Distance(obj.transform.position, transform.position));
            if (dist < distance)
            {
                distance = dist;
                temp = obj;
            }
        }

        // Assigns closest object;
        ClosestObject = temp;

        // If it's already the object that is the closest then return
        if (GetComponentInParent<PlayerObject>().ItemNearby == ClosestObject || GetComponentInParent<PlayerObject>().BoxNearby == ClosestObject)
        {
            return;
        }

        // else check whether object is an Item or Lootbox
        if (ClosestObject != null)
        {
            if (ClosestObject.CompareTag("Item"))
            {
                GetComponentInParent<PlayerObject>().ItemNearby = ClosestObject;
            }
            else if (ClosestObject.CompareTag("Lootbox"))
            {
                GetComponentInParent<PlayerObject>().BoxNearby = ClosestObject;
            }
        }

    }

}
