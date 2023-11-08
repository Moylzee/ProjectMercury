using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionScript : MonoBehaviour
{
    public GameObject InteractPrompt;

    private int triggerCount = 0;
    private List<GameObject> ClosestItemToPlayer;
    private GameObject ClosestObject;

    private void Start()
    {
        InteractPrompt.GetComponent<Text>().text = Settings.GetData().GetKey_InteractKey().ToUpper();
        ClosestItemToPlayer = new List<GameObject>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            triggerCount++;
            ClosestItemToPlayer.Add(collision.gameObject);

            if (triggerCount == 1)
            {
                // Enable Interact Prompt only if this is the first trigger
                InteractPrompt.SetActive(true);
            }
        }
        if (collision.gameObject.CompareTag("Lootbox"))
        {
            triggerCount++;
            ClosestItemToPlayer.Add(collision.gameObject);
            if (triggerCount == 1)
            {
                // Enable Interact Prompt only if this is the first trigger
                InteractPrompt.SetActive(true);
            }
        }

        FindClosestObject();
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            triggerCount--;
            ClosestItemToPlayer.Remove(collision.gameObject);


            if (GetComponentInParent<PlayerObject>().ItemNearby == collision.gameObject)
            {
                GetComponentInParent<PlayerObject>().ItemNearby = null;
            }

            FindClosestObject();
            if (triggerCount <= 0)
            {
                // Disable Interact Prompt when no relevant triggers are left
                InteractPrompt.SetActive(false);
                GetComponentInParent<PlayerObject>().ItemNearby = null;
            }
        }
        else if (collision.gameObject.CompareTag("Lootbox"))
        {
            triggerCount--;
            ClosestItemToPlayer.Remove(collision.gameObject);

            if (GetComponentInParent<PlayerObject>().BoxNearby == collision.gameObject)
            {
                GetComponentInParent<PlayerObject>().BoxNearby = null;
            }

            FindClosestObject();
            if (triggerCount <= 0)
            {
                // Disable Interact Prompt when no relevant triggers are left
                InteractPrompt.SetActive(false);
                GetComponentInParent<PlayerObject>().BoxNearby = null;
            }
        }
    }


    private void FindClosestObject()
    {
        if(ClosestItemToPlayer.Count <= 0)
        {
            return;
        }

        GameObject temp = null;
        float distance = 999f;

        foreach(GameObject obj in ClosestItemToPlayer)
        {
            float dist = (Vector2.Distance(obj.transform.position, transform.position));
            if (dist < distance)
            {
                distance = dist;
                temp = obj;
            }
        }

        ClosestObject = temp;


        if (GetComponentInParent<PlayerObject>().ItemNearby == ClosestObject || GetComponentInParent<PlayerObject>().BoxNearby == ClosestObject)
        {
            return;
        }
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
