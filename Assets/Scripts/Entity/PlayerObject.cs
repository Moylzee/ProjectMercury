
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerObject : GameEntity
{

    public GameObject InventoryPrefab;
    public GameObject weaponDetailsPrefab;
    public GameObject InteractPrompt;
    public GameObject BoxNearby;
    private GameObject ItemNearby;
    public PlayerInventory Inventory { get; set; }
    public bool BOX_OPEN = false;
    public Vector2 InteractHint_Offset = new Vector2(-3f, 5f);
    public float RANGE_OF_PICKUP = 12f;
    private bool InteractHint_IsVisible;
    private PlayerShootingBehaviour ShootingBehaviour;
    private CircleCollider2D RangeCollider;

    void Start()
    {

        Inventory = new PlayerInventory(InventoryPrefab, weaponDetailsPrefab);
        InteractPrompt = GameObject.FindWithTag("InteractKey_Hint");
        InteractPrompt.GetComponent<Text>().text = Settings.GetData().GetKey_InteractKey().ToUpper();
        RangeCollider = GameObject.FindWithTag("Range").GetComponent<CircleCollider2D>();

        ShootingBehaviour = GetComponentInChildren<PlayerShootingBehaviour>();

    }

    /* On Update, Checks shooting, keyboard input, and interaction */
    void Update()
    {
        this.PlayerShootHandler();
        this.PlayerKeyboardHandler();
        InvokeRepeating("InteractionHintHandler", 0, 0.2f);
        this.InteractionHint();
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
                return;
            }
            // If weapon in reload state, stop
            GetComponentInChildren<PlayerShootingBehaviour>().StopReloading();
            Inventory.EquipWeapon(weapon);

        }
        else if (Input.GetKeyDown(Settings.GetData().GetKey_WeaponSlot2()))
        {
            // Equip secondary weapon
            Weapon weapon = (Weapon)Inventory.GetSlot("5").GetItemInSlot();
            if (weapon == null)
            {
                Inventory.UnequipWeapon();
                return;
            }
            // if weapon in reload state, stop
            GetComponentInChildren<PlayerShootingBehaviour>().StopReloading();
            Inventory.EquipWeapon(weapon);

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
            Item item = ItemNearby.GetComponent<WeaponObjectBehaviour>().item;
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
                Inventory.EquipWeapon((Weapon)item);
                return;
            }
            slot = Inventory.GetSlot("5");
            if (slot.IsSlotEmpty())
            {
                slot.InsertItemInSlot(item);
                Destroy(ItemNearby);
                ItemNearby = null;
                Inventory.EquipWeapon((Weapon)item);
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
        }
    }

    /* Interaction Hint logic */
    void InteractionHint()
    {
        if (InteractHint_IsVisible)
        {
            InteractPrompt.GetComponent<Text>().rectTransform.position =
                Camera.main.WorldToScreenPoint((Vector2)transform.position + InteractHint_Offset);
        }
        else
        {
            InteractPrompt.GetComponent<Text>().rectTransform.position = new Vector2(-1500, 1500);
        }
    }

    void InteractionHintHandler()
    {


        
        InteractHint_IsVisible = false;
        ItemNearby = null;
        BoxNearby = null;



        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items)
        {
            if (RangeCollider.OverlapPoint(item.transform.position))
            {

                InteractHint_IsVisible = true;
                ItemNearby = item;

            }
        }

        
        GameObject[] lootboxes = GameObject.FindGameObjectsWithTag("Lootbox");
        foreach(GameObject box in lootboxes)
        {
            if(RangeCollider.OverlapPoint(box.transform.position))
            {
                InteractHint_IsVisible = true;
                BoxNearby = box;
            }
        }
        
    
    }
    public PlayerInventory GetPlayerInventory()
    {
        return Inventory;
    }

}


