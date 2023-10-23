
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
    public float RANGE_OF_PICKUP = 12f;

    public PlayerInventory Inventory { get; set; }

    public Vector2 InteractHint_Offset = new Vector2(-3f, 5f);

    private bool InteractHint_IsVisible;
    private GameObject ItemNearby;
    public GameObject BoxNearby;
    public bool BOX_OPEN = false;

    private PlayerShootingBehaviour ShootingBehaviour;


    // Player entry point
    void Start()
    {

        Inventory = new PlayerInventory(InventoryPrefab, weaponDetailsPrefab);
        InteractPrompt = GameObject.FindWithTag("InteractKey_Hint");
        InteractPrompt.GetComponent<Text>().text = Settings.GetData().GetKey_InteractKey().ToUpper();


        ShootingBehaviour = GetComponentInChildren<PlayerShootingBehaviour>();

    }


    public PlayerInventory GetPlayerInventory()
    {
        return Inventory;
    }

    void Update()
    {

        this.PlayerShootHandler();
        this.PlayerKeyboardHandler();

        InvokeRepeating("InteractionHintHandler", 0, 0.2f);

        this.InteractionHint();

    }

    void PlayerShootHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Inventory.IsHoveringOverSlot() != null || BOX_OPEN == true)
            {
                // unable to shoot atm
                return;
            }

            if (Inventory.getEquippedWeapon() != null)
            {
                ShootingBehaviour.Shoot();
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            if (Inventory.getEquippedWeapon() != null)
            {
                ShootingBehaviour.StopShoot();
            }
        }
    }

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

            Inventory.EquipWeapon(weapon);

        }

        // Consumables
        if (Input.GetKeyDown(Settings.GetData().GetKey_ItemSlot1()))
        {
            // Use slot 1 Consumable
        }
        else if (Input.GetKeyDown(Settings.GetData().GetKey_ItemSlot2()))
        {
            // Use slot 2 Consumable
        }
        else if (Input.GetKeyDown(Settings.GetData().GetKey_ItemSlot3()))
        {
            // Use slot 3 Consumable
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

    }


    void BoxOpen()
    {
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

            Debug.Log("Inventory is full!");
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

            Debug.Log("Inventory is full!");
            return;
        }
    }


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
            if (Vector2.Distance(item.transform.position, transform.position) < RANGE_OF_PICKUP)
            {

                InteractHint_IsVisible = true;
                ItemNearby = item;

            }
        }

        GameObject[] lootboxes = GameObject.FindGameObjectsWithTag("Lootbox");
        foreach (GameObject box in lootboxes)
        {
            if (Vector2.Distance(box.transform.position, transform.position) < RANGE_OF_PICKUP * 2f)
            {
                InteractHint_IsVisible = true;
                BoxNearby = box;
            }
        }
    }


}


