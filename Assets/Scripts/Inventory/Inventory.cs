using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{

    private List<Item> Items { get; }

    protected uint InventorySize { get; set; }

    public Inventory()
    {
        Items = new List<Item>();
    }

    void Update()
    {
        
    }

    public void AddItem(Item item)
    {
        if (Items.Count >= InventorySize)
        {
            Debug.Log("Inventory is full!");
            // Prompt the user somehow that the inventory.
            return;
        }
        Items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        if (!Items.Contains(item))
        {
            Debug.Log("Inventory doesn't contain item: " + item.GetItemName());
            return;
        }
        Items.Remove(item);
    }

    public Item GetItem(int index)
    {

        if (Items.Count <= 0)
        {
            Debug.Log("Inventory is empty");
            return null;
        }

        return Items[index];
    }

    public void debug_ItemsInInvetory()
    {
        foreach (Item item in Items)
        {
            Debug.Log("Item name: " + item.GetItemName());
        }
    }

}


public class PlayerInventory : Inventory
{
    public Weapon EquippedWeapon = null;
    public List<GameObject> InventorySlotList = new();
    public Dictionary<string, SlotItem> Slots = new();


    private GameObject WeaponDetails;

    public PlayerInventory(GameObject InventoryPrefab, GameObject WeaponDetailsPrefab)
    {
        GameObject InventoryObject = Instantiate(InventoryPrefab);
        WeaponDetails = Instantiate(WeaponDetailsPrefab);
        InventoryObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
        WeaponDetails.transform.SetParent(GameObject.Find("Canvas").transform, false);
        InventorySlotList.Add(GameObject.FindWithTag("Slot1"));
        InventorySlotList.Add(GameObject.FindWithTag("Slot2"));
        InventorySlotList.Add(GameObject.FindWithTag("Slot3"));
        InventorySlotList.Add(GameObject.FindWithTag("Slot4"));
        InventorySlotList.Add(GameObject.FindWithTag("Slot5"));

        WeaponDetails.SetActive(false);

        LoadDictionary();
    }


    public Weapon getEquippedWeapon()
    {
        return EquippedWeapon;
    }


   public void UpdateDetails()
    {
        if(getEquippedWeapon() == null)
        {
            return;
        }
        string ammo = getEquippedWeapon().GetBulletsInMag() + " / " + getEquippedWeapon().GetSpareAmmo();
        WeaponDetails.transform.Find("Ammo").GetComponent<TextMeshProUGUI>().text = ammo;
        WeaponDetails.transform.Find("Weapon").GetComponent<TextMeshProUGUI>().text = getEquippedWeapon().GetItemName();
    }

    public void EquipWeapon(Weapon weapon)
    {

        if (weapon == null)
        {
            return;
        }

        if (this.EquippedWeapon != null)
        {
            this.UnequipWeapon();
        }

        WeaponDetails.SetActive(true);



        this.EquippedWeapon = weapon;
        UpdateDetails();
    }

    public void UnequipWeapon()
    {
        this.EquippedWeapon = null;

        WeaponDetails.SetActive(false);
    }


    public GameObject IsHoveringOverSlot()
    {
        if (InventorySlotList.Count <= 0)
        {
            Debug.Log("Inventory Slot list is empty");
            return null;
        }

        foreach (GameObject slot in InventorySlotList)
        {
            if (slot.GetComponent<SlotItem>().IsHoveringOverSlot())
            {
                return slot;
            }
        }
        return null;
    }

    void LoadDictionary()
    {
        for (int i = 0; i < InventorySlotList.Count; i++)
        {
            Slots.Add((i + 1).ToString(), InventorySlotList[i].GetComponent<SlotItem>());
        }
    }

    public SlotItem GetSlot(string slot)
    {
        if (Slots.TryGetValue(slot, out SlotItem item))
        {
            return item;
        }
        else
        {
            Debug.LogError("Failed to retrieve slot");
            return null;
        }
    }

}