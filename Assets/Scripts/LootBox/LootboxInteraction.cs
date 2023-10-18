using UnityEngine;
using UnityEngine.UI;

public class LootboxInteraction : MonoBehaviour
{
    public GameObject InventoryPrefab;

    private GameObject BoxInventory;

    private GameObject Player;

    private RarityClass Rarity; // 0 - 1, How good is a given box

    private GameObject InventorySlots;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        transform.SetParent(GameObject.FindWithTag("LootboxParent").transform); // set parent
        BoxInventory = GameObject.Instantiate(InventoryPrefab); // Create inventory prefab

        BoxInventory.transform.SetParent(GameObject.Find("Canvas").transform, false);

        BoxInventory.SetActive(false); // Disable box inventory until player opens it

        Rarity = RaritySelection(); // Rarity of the lootbox

        LoadInventorySlots();
    }

    void Update()
    {
        if (BoxInventory.activeSelf)
        {
            if (Vector2.Distance(transform.position, Player.transform.position) > Player.GetComponent<PlayerObject>().RANGE_OF_PICKUP * 1.8f)
            {
                CloseBox();
            }
        }
    }

    /*
     * Randomly selects a number from 0 - 1, 
     */
    private RarityClass RaritySelection()
    {
        float number = Random.Range(0f, 1f);
        if (number < 0.5)
        {
            return RarityClass.COMMON;
        }
        else if (number < 0.75)
        {
            return RarityClass.UNCOMMON;
        }
        else if (number < 0.92)
        {
            return RarityClass.RARE;
        }
        return RarityClass.LEGENDARY;
    }

    void FillBox()
    {

    }

    public void OpenBox()
    {
        BoxInventory.SetActive(true);
    }

    public void CloseBox()
    {
        BoxInventory.SetActive(false);
        Player.GetComponent<PlayerObject>().BOX_OPEN = false;
    }


    void LoadInventorySlots()
    {
        Transform container = BoxInventory.transform.Find("ItemsBackpanel");
        if (container == null)
        {
            Debug.LogError("Fuck 1");
            return;
        }

        Transform Slot1 = container.Find("Slot1");
        var Item1 = Slot1.Find("Item1").GetComponent<SlotItem>();

        if (Item1 == null)
        {
            Debug.LogError("WHY..>");
            return;
        }

        if (Item1.IsHoveringOverSlot())
        {
            Debug.Log("omg this worked first time");
        }


    }

}


public enum RarityClass
{
    COMMON, UNCOMMON, RARE, LEGENDARY
}