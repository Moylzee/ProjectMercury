
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


/*
 * PlayerObject Script
 * Main script for player responsible for 
 *      * player inventory, 
 *      * nearby items and lootbox detection,
 *      * keyboard input
 */
public class PlayerObject : MonoBehaviour
{

    /* Plyaer gameobject references */
    public GameObject InventoryPrefab;
    public GameObject weaponDetailsPrefab;
    public GameObject BoxNearby;
    public GameObject ItemNearby;
    public PlayerInventory Inventory;
    private PlayerShootingBehaviour ShootingBehaviour;


    public bool BOX_OPEN = false;
    public Vector2 InteractHint_Offset = new(-3f, 5f);
    
    public float RANGE_OF_PICKUP = 12f;

    private const int ENEMY_DAMAGE_POINTS = 10;
    private const int ENEMY_KILL_POINTS = 100;

    private int Level = 1;

    private static bool loadedDefaults = false;


    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Inventory = new PlayerInventory(InventoryPrefab, weaponDetailsPrefab);
        ShootingBehaviour = GetComponentInChildren<PlayerShootingBehaviour>();

        if (!loadedDefaults)
        {
            // Set default values
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.SetInt("ENEMY_DAMAGE_POINTS", ENEMY_DAMAGE_POINTS);
            PlayerPrefs.SetInt("ENEMY_KILL_POINTS", ENEMY_KILL_POINTS);
            PlayerPrefs.SetInt("Level", Level);
            loadedDefaults = true;
        }
        


        // If there is a static inventory reference
        if (PlayerInventory.StaticInventoryWeapon.Count > 0)
        {
            // Update current inventory with saved items,
            Inventory.GetSlot("1").InsertItemInSlot(PlayerInventory.GetNextConsumableItem());
            Inventory.GetSlot("2").InsertItemInSlot(PlayerInventory.GetNextConsumableItem());
            Inventory.GetSlot("3").InsertItemInSlot(PlayerInventory.GetNextConsumableItem());

            Inventory.GetSlot("4").InsertItemInSlot(PlayerInventory.GetNextWeapon());
            Inventory.GetSlot("5").InsertItemInSlot(PlayerInventory.GetNextWeapon());

            // Clear the static references
            PlayerInventory.StaticInventoryConsumable.Clear();
            PlayerInventory.StaticInventoryWeapon.Clear();
        }

    }

    /* On Update, Checks shooting, keyboard input, and interaction */
    void Update()
    {
        this.PlayerShootHandler();
        this.PlayerKeyboardHandler();
    }

    /* Logic for handling player shooting */
    void PlayerShootHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Unable to shoot as in Lootbox
            if (Inventory.IsHoveringOverSlot() != null || BOX_OPEN == true)
            {
                // unable to shoot atm
                return;
            }

            // Shoot
            if (Inventory.getEquippedWeapon() != null)
            {
                ShootingBehaviour.Shoot();
            }
        }

        // Stop shoot
        if(Input.GetMouseButtonUp(0))
        {
            if (Inventory.getEquippedWeapon() != null)
            {
                ShootingBehaviour.StopShoot();
            }
        }
    }
    /* Keyboard Handler */
    void PlayerKeyboardHandler()
    {


        // Weapons
        if (Input.GetKeyDown(Settings.GetData().GetKey_WeaponSlot1()))
        {
            // Equip primary weapon
            Weapon weapon = (Weapon)Inventory.GetSlot("4").GetItemInSlot();
            if (weapon == null)
            {
                Inventory.UnequipWeapon();
                EquipVisualWeapon();
                return;
            }
            else if (Inventory.getEquippedWeapon() != null && Inventory.getEquippedWeapon().UID == weapon.UID)
            {
                return;
            }
            Weapon weaponCopy = new();
            weaponCopy.DeepReadWeapon(weapon);
            // If weapon in reload state, stop
            GetComponentInChildren<PlayerShootingBehaviour>().StopReloading();
            Inventory.EquipWeapon(weaponCopy);
            EquipVisualWeapon();
        }
        else if (Input.GetKeyDown(Settings.GetData().GetKey_WeaponSlot2()))
        {
            // Equip secondary weapon
            Weapon weapon = (Weapon)Inventory.GetSlot("5").GetItemInSlot();
            if (weapon == null)
            {
                Inventory.UnequipWeapon();
                EquipVisualWeapon();
                return;
            }else if(Inventory.getEquippedWeapon() != null && Inventory.getEquippedWeapon().UID == weapon.UID)
            {
                return;
            }
            Weapon weaponCopy = new();
            weaponCopy.DeepReadWeapon(weapon);
            // if weapon in reload state, stop
            GetComponentInChildren<PlayerShootingBehaviour>().StopReloading();
            Inventory.EquipWeapon(weaponCopy);
            EquipVisualWeapon();

        }

        // Consumables
        if (Input.GetKeyDown(Settings.GetData().GetKey_ItemSlot1()))
        {
            // Use slot 1 Consumable
            ConsumableItem item = (ConsumableItem)Inventory.GetSlot("1").GetItemInSlot();
            if (item == null)
            {
                return;
            }

            // We have to create the game object of said Item
            var ItemObject = ConsumableItemLoader.CreateConsumableItem(new Vector2(-1000, -1000), item);
            ItemObject.GetComponent<ConsumableItemObjectBehaviour>().UseEffect();
            Destroy(ItemObject);
            Inventory.GetSlot("1").RemoveItemFromSlot();

        }
        else if (Input.GetKeyDown(Settings.GetData().GetKey_ItemSlot2()))
        {
            // Use slot 2 Consumable
            ConsumableItem item = (ConsumableItem)Inventory.GetSlot("2").GetItemInSlot();
            if (item == null)
            {
                return;
            }

            // We have to create the game object of said Item
            var ItemObject = ConsumableItemLoader.CreateConsumableItem(new Vector2(-1000, -1000), item);
            ItemObject.GetComponent<ConsumableItemObjectBehaviour>().UseEffect();
            Destroy(ItemObject);
            Inventory.GetSlot("2").RemoveItemFromSlot();
        }
        else if (Input.GetKeyDown(Settings.GetData().GetKey_ItemSlot3()))
        {
            // Use slot 3 Consumable
            ConsumableItem item = (ConsumableItem)Inventory.GetSlot("3").GetItemInSlot();
            if (item == null)
            {
                return;
            }

            // We have to create the game object of said Item
            var ItemObject = ConsumableItemLoader.CreateConsumableItem(new Vector2(-1000, -1000), item);
            ItemObject.GetComponent<ConsumableItemObjectBehaviour>().UseEffect();
            Destroy(ItemObject);
            Inventory.GetSlot("3").RemoveItemFromSlot();
        }


        // Pick up item from ground
        if (Input.GetKeyDown(Settings.GetData().GetKey_InteractKey()))
        {

            // No item nearby to pick up
            if (!ItemNearby && !BoxNearby)
            {
                return;
            }

            if (ItemNearby)
            {
                ItemPickUp();
            }
            else if (BoxNearby)
            {
                BoxOpen();
            }
        }

        // Reload key
        if(Input.GetKeyDown(Settings.GetData().GetKey_ReloadWeapon()))
        {
            if(Inventory.getEquippedWeapon() == null)
            {
                return;
            }
            GetComponentInChildren<PlayerShootingBehaviour>().Reload();
        }
    }

    /*  Open Lootbox */
    void BoxOpen()
    {
        // Loot box open
        if (!BOX_OPEN)
        {
            BoxNearby.GetComponent<LootboxInteraction>().OpenBox();
            BOX_OPEN = true;
        }
        else
        {
            BoxNearby.GetComponent<LootboxInteraction>().CloseBox();
            BOX_OPEN = false;
        }
    }

    /* Pick item up*/
    void ItemPickUp()
    {
        if (ItemNearby.GetComponent<WeaponObjectBehaviour>() != null)
        {
            Weapon item = ItemNearby.GetComponent<WeaponObjectBehaviour>().item;

            if (!item.IsOnGround())
            {
                return;
            }

            var slot = Inventory.GetSlot("4");

            if (slot.IsSlotEmpty())
            {
                slot.InsertItemInSlot(item);
                Destroy(ItemNearby);
                ItemNearby = null;
                Inventory.EquipWeapon(item);
                EquipVisualWeapon();
                return;
            }
            slot = Inventory.GetSlot("5");
            if (slot.IsSlotEmpty())
            {
                slot.InsertItemInSlot(item);
                Destroy(ItemNearby);
                ItemNearby = null;
                Inventory.EquipWeapon(item);
                EquipVisualWeapon();
                return;
            }
            return;
        }

        else if (ItemNearby.GetComponent<ConsumableItemObjectBehaviour>() != null)
        {
            Item item = ItemNearby.GetComponent<ConsumableItemObjectBehaviour>().item;
            if (!item.IsOnGround())
            {
                return;
            }

            var slot = Inventory.GetSlot("1");
            if (slot.IsSlotEmpty())
            {
                slot.InsertItemInSlot(item);
                Destroy(ItemNearby);
                ItemNearby = null;
                return;
            }
            slot = Inventory.GetSlot("2");
            if (slot.IsSlotEmpty())
            {
                slot.InsertItemInSlot(item);
                Destroy(ItemNearby);
                ItemNearby = null;
                return;
            }
            slot = Inventory.GetSlot("3");
            if (slot.IsSlotEmpty())
            {
                slot.InsertItemInSlot(item);
                Destroy(ItemNearby);
                ItemNearby = null;
                return;
            }
            return;
        }else if(ItemNearby.GetComponent<ItemBonusObjectBehaviour>() != null)
        {
            ItemNearby.GetComponent<ItemBonusObjectBehaviour>().item.UseEffect();
            Destroy(ItemNearby, 0.1f);
        }
    }

    /* Method to visually equip the weapon */
    public void EquipVisualWeapon()
    {
        var image = transform.Find("RotatePoint").transform.Find("Equipped").GetComponent<SpriteRenderer>();
        if(image == null)
        {
            Debug.LogError("SpriteRenderer not found in RotatePoint/Equipped!");
        }

        // if no weapon is in hand, clear image sprite
        if(Inventory.getEquippedWeapon() == null)
        {
            image.sprite = null;
            return;
        }
        // weapon is in hand, set weapon to the image of weapon
        image.sprite = Resources.Load<Sprite>("Weapons/" + Inventory.getEquippedWeapon().GetImageSource());
    }



    public PlayerInventory GetPlayerInventory()
    {
        return Inventory;
    }

}


