using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            // Prompt the user somehow that the inventory.
            return;
        }
        Items.Add(item);
    }

    public void RemoveItem(Item item)
    {
        if (!Items.Contains(item))
        {
            return;
        }
        Items.Remove(item);
    }

    public Item GetItem(int index)
    {

        if (Items.Count <= 0)
        {
            return null;
        }

        return Items[index];
    }

}


public class PlayerInventory : Inventory
{
    public Weapon EquippedWeapon = null;
    public List<GameObject> InventorySlotList = new();
    public Dictionary<string, SlotItem> Slots = new();

    private GameObject WeaponDetails;

    /* Ammo types */
    private uint LightAmmo = 0;
    private uint HeavyAmmo = 0;
    private uint ShotgunAmmo = 0;

    /* Weapon tier sprites */
    private Sprite Sprite_Bronze;
    private Sprite Sprite_Silver;
    private Sprite Sprite_Gold;
    private Sprite Sprite_Diamond;

    /* Ammo Type sprites*/
    private Sprite Sprite_LightAmmo;
    private Sprite Sprite_HeavyAmmo;
    private Sprite Sprite_ShotgunAmmo;


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

        /* Load Sprites for Weapon tiers*/
        Sprite_Bronze = Resources.Load<Sprite>("Misc/WeaponTiers/Bronze");
        Sprite_Silver = Resources.Load<Sprite>("Misc/WeaponTiers/Silver");
        Sprite_Gold = Resources.Load<Sprite>("Misc/WeaponTiers/Gold");
        Sprite_Diamond = Resources.Load<Sprite>("Misc/WeaponTiers/Diamond");

        /* Load Sprites for Ammo Types */
        Sprite_LightAmmo = Resources.Load<Sprite>("Misc/WeaponTiers/LightAmmo");
        Sprite_HeavyAmmo = Resources.Load<Sprite>("Misc/WeaponTiers/HeavyAmmo");
        Sprite_ShotgunAmmo = Resources.Load<Sprite>("Misc/WeaponTiers/ShotgunAmmo");

        WeaponDetails.SetActive(false);

        LoadDictionary();
    }


    public Weapon getEquippedWeapon()
    {
        return EquippedWeapon;
    }


    public void UpdateDetails()
    {
        if (getEquippedWeapon() == null)
        {
            return;
        }

        string ammo = getEquippedWeapon().GetBulletsInMag() + " / " + GetWeaponSpareAmmoBasedOnCategory();
        string weaponName = getEquippedWeapon().GetItemName() + " (" + getEquippedWeapon().GetWeaponCategory() + ")";

        WeaponDetails.transform.Find("Ammo").GetComponent<TextMeshProUGUI>().text = ammo;
        WeaponDetails.transform.Find("Weapon").GetComponent<TextMeshProUGUI>().text = weaponName;

        WeaponDetails.transform.Find("TierImage").GetComponent<Image>().sprite = GetSpriteBasedOnTier();
        WeaponDetails.transform.Find("Damage").GetComponent<Slider>().value = getEquippedWeapon().GetDamageDealtPerBullet();
        WeaponDetails.transform.Find("AmmoImage").GetComponent<Image>().sprite = GetSpriteBasedOnAmmoType();
    }

    public Sprite GetSpriteBasedOnTier()
    {
        switch (getEquippedWeapon().GetItemRarity())
        {
            case 1:
                return Sprite_Bronze;
            case 2:
                return Sprite_Silver;
            case 3:
                return Sprite_Gold;
            case 4:
                return Sprite_Diamond;
            default:
                Debug.LogError("Could not return sprite for weapon tier");
                return Sprite_Bronze;
        }

    }

    Sprite GetSpriteBasedOnAmmoType()
    {
        switch (getEquippedWeapon().GetWeaponCategory())
        {
            case "AR":
            case "Pistol":
                return Sprite_LightAmmo;
            case "Rifle":
            case "LMG":
                return Sprite_HeavyAmmo;
            case "Shotgun":
                return Sprite_ShotgunAmmo;
            default:
                Debug.LogWarning("Using default ammo type image");
                return Sprite_LightAmmo;
        }
    }
    public uint GetWeaponSpareAmmoBasedOnCategory()
    {
        /* Switch statement*/
        return getEquippedWeapon().GetWeaponCategory() switch
        {
            "AR" or "Pistol" => GetLightAmmo(),
            "Rifle" or "LMG" => GetHeavyAmmo(),
            "Shotgun" => GetShotgunAmmo(),
            _ => 0,
        };
    }

    /* Method to set ammo based on the type of weapon the player has
     * Static as weapon is a reference
     */
    public static void SetWeaponSpareAmmoBasedOnCategory(PlayerInventory inven, uint ammo)
    {
        /* Switch statement*/
        switch (inven.getEquippedWeapon().GetWeaponCategory())
        {
            case "AR":
            case "Pistol":
                inven.SetLighAmmo(ammo);
                break;
            case "Rifle":
            case "LMG":
                inven.SetHeavyAmmo(ammo);
                break;
            case "Shotgun":
                inven.SetShotgunAmmo(ammo);
                break;
            default:
                break;
        }
    }

    /*  Equip weapon */
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
        AddAmmo();
        UpdateDetails();
    }

    public void UnequipWeapon()
    {
        this.EquippedWeapon = null;

        WeaponDetails.SetActive(false);
    }

    /* Method to Get the weapon category and add its spare ammo to player ammo type */
    void AddAmmo()
    {
        string weaponType = this.getEquippedWeapon().GetWeaponCategory();

        switch (weaponType)
        {
            case "AR":
            case "Pistol":
                this.AddLightAmmo(this.getEquippedWeapon().GetSpareAmmo());

                break;
            case "Rifle":
            case "LMG":
                this.AddHeavyAmmo(this.getEquippedWeapon().GetSpareAmmo());
                break;
            case "Shotgun":
                this.AddShotgunAmmo(this.getEquippedWeapon().GetSpareAmmo());
                break;
            default:
                Debug.LogWarning("Unknown weapon category tried adding ammo");
                break;
        }

        this.getEquippedWeapon().SetSpareAmmo(0);

    }

    public GameObject IsHoveringOverSlot()
    {
        if (InventorySlotList.Count <= 0)
        {
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

    public void SetLighAmmo(uint ammo)
    {
        this.LightAmmo = ammo;
    }
    public void SetHeavyAmmo(uint ammo)
    {
        this.HeavyAmmo = ammo;
    }
    public void SetShotgunAmmo(uint ammo)
    {
        this.ShotgunAmmo = ammo;
    }

    public uint GetLightAmmo() { return this.LightAmmo; }
    public uint GetHeavyAmmo() { return this.HeavyAmmo; }
    public uint GetShotgunAmmo() { return this.ShotgunAmmo; }

    public void AddLightAmmo(uint ammo)
    {
        this.LightAmmo += ammo;
    }
    public void AddHeavyAmmo(uint ammo)
    {
        this.HeavyAmmo += ammo;
    }
    public void AddShotgunAmmo(uint ammo)
    {
        this.ShotgunAmmo += ammo;
    }
}