
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * The item Object Behavior Class is attached to the items Prefab
 * It gives the item class a gameobject bound to the data
 * 
 * Main Reason for this is that Unity Game Objects (inheriting MonoBehaviour) are not Serializable
 *  hence cannot be put into or read from a json file.
 */
public class WeaponObjectBehaviour : ItemObjectBehaviour<Weapon>
{

    public override void Update()
    {

        base.BaseUpdate();
        this.InventoryLogic();


    }


    public override void InventoryLogic()
    {
        if (item.IsPickedUp())
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + item.GetOffset();
        }

        if (Input.GetMouseButtonUp(0) && item.IsPickedUp())
        {
            var slot = GameObject.FindWithTag("Slot4").GetComponent<SlotItem>();
            if (slot.IsHoveringOverSlot() && slot.IsSlotEmpty())
            {
                PlayerScript.Inventory.EquipWeapon(item);
                slot.InsertItemInSlot(item);
                Destroy(gameObject);
                return;
            }

            slot = GameObject.FindWithTag("Slot5").GetComponent<SlotItem>();
            if (slot.IsHoveringOverSlot() && slot.IsSlotEmpty())
            {
                PlayerScript.Inventory.EquipWeapon(item);
                slot.InsertItemInSlot(item);
                Destroy(gameObject);
                return;
            }




            if (PlayerScript.Inventory.getEquippedWeapon() != null && PlayerScript.Inventory.getEquippedWeapon().UID == item.UID)
            {
                PlayerScript.Inventory.UnequipWeapon();
            }
            item.SetPickedUp(false);
            item.SetOnGround(true);
            SetOnGround(true);
            transform.position = (Vector2)GameObject.FindWithTag("Player").transform.position;

        }
    }
}
