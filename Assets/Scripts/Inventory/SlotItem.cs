using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class SlotItem : MonoBehaviour, IPointerDownHandler
{
    private Button slotButton;
    private Image buttonImage;

    public bool HoveringOverSlot = false;

    private Item itemInSlot;

    void Awake()
    {
        slotButton = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry enterEntry = new EventTrigger.Entry();
        enterEntry.eventID = EventTriggerType.PointerEnter;
        enterEntry.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });
        eventTrigger.triggers.Add(enterEntry);

        // Add PointerExit callback
        EventTrigger.Entry exitEntry = new EventTrigger.Entry();
        exitEntry.eventID = EventTriggerType.PointerExit;
        exitEntry.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });
        eventTrigger.triggers.Add(exitEntry);
    }


    void OnPointerEnter(PointerEventData eventData)
    {
        HoveringOverSlot = true;
    }

    void OnPointerExit(PointerEventData eventData)
    {
        HoveringOverSlot = false;
    }


    public bool IsHoveringOverSlot() { return HoveringOverSlot; }

    public Button GetButtonComponent()
    {
        return slotButton;
    }

    public void InsertItemInSlot(Item item)
    {
        if (item == null)
        {
            Debug.LogError("Item inserted into slot is null");
            return;
        }

        itemInSlot = item; // Assign weapon to slot

        buttonImage.sprite = item.GetSpriteRenderer().sprite;
        slotButton.onClick.AddListener(() => func());
        buttonImage.color = ColorLoader.HexToColor("#fff");


    }

    void func()
    {

    }


    public void RemoveItemFromSlot()
    {
        buttonImage.sprite = null; // Remove image from slot

        itemInSlot = null; // remove item from slot
        slotButton.onClick.RemoveAllListeners();
        buttonImage.color = ColorLoader.HexToColor("#5A5653");
    }

    public Item GetItemInSlot()
    {
        return itemInSlot;
    }

    public bool IsSlotEmpty()
    {
        if (itemInSlot == null)
        {
            return true;
        }
        return false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (itemInSlot == null)
            {
                return;
            }
            Debug.Log("Item : " + itemInSlot.GetItemName() + " is being removed from slot");


            itemInSlot.SetPickedUp(true); // Set the item to be picked up
            itemInSlot.SetOffset(Camera.main.ScreenToWorldPoint(transform.position) - Camera.main.ScreenToWorldPoint(Input.mousePosition));

            if (itemInSlot.ItemType.Equals("Weapon"))
            {
                Weapon w = (Weapon)itemInSlot;
                WeaponLoader.CreateWeaponObject((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), w);
            }
            else if (itemInSlot.ItemType.Equals("Consumable"))
            {
                ConsumableItem i = (ConsumableItem)itemInSlot;
                ConsumableItemLoader.CreateConsumableItem((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), i);
            }


            RemoveItemFromSlot();
        }
    }
}
