using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SlotItem : MonoBehaviour, IPointerDownHandler
{

    private Image buttonImage;

    public bool hoveringOverSlot = false;

    private Weapon weaponInSlot;
    private ConsumableItem consumableInSlot;

    void Awake()
    {
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

    /* hovering over the slot */
    public void OnPointerEnter(PointerEventData eventData)
    {
        hoveringOverSlot = true;
    }
   
    /* hover exit from slot */
    public void OnPointerExit(PointerEventData eventData)
    {
        hoveringOverSlot = false;
    }

    /* Clicking on the slot */
    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            // No item in slot
            if(weaponInSlot == null && consumableInSlot == null)
            {
                return;
            }

            // Pick up weapon
            if(weaponInSlot != null)
            {
                PickUpWeapon();
                return;
            }
            PickUpConsumable();
            return;
        }
    }

    /* Pick up weapon item from inventory */
    private void PickUpWeapon()
    {
        weaponInSlot.SetPickedUp(true);
        weaponInSlot.SetOffset(Camera.main.ScreenToWorldPoint(transform.position) - Camera.main.ScreenToWorldPoint(Input.mousePosition));

        WeaponLoader.CreateWeaponObject((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), weaponInSlot);
        RemoveItemFromSlot();
    }

    /* Pick up consumable item from inventory */
    private void PickUpConsumable()
    {
        consumableInSlot.SetPickedUp(true);
        consumableInSlot.SetOffset(Camera.main.ScreenToWorldPoint(transform.position) - Camera.main.ScreenToWorldPoint(Input.mousePosition));

        ConsumableItemLoader.CreateConsumableItem((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), consumableInSlot);
        RemoveItemFromSlot();
    }

    public bool IsHoveringOverSlot() { return hoveringOverSlot; }

    /* Inserts item into slot */
    public void InsertItemInSlot(Item item)
    {
        if (item == null)
            return;


        SetSlotColorOnInsert();

        // Check if item is weapon or consumable
        if(item is Weapon weapon)
        {
            InsertWeaponIntoSlot(weapon);
        }else if (item is ConsumableItem consumable)
        {
            InsertConsumableIntoSlot(consumable);
        }

    }


    /* Sets the slot colour to non-transparent */
    private void SetSlotColorOnInsert()
    {
        Color btnColor = buttonImage.color;
        btnColor.a = 255;
        buttonImage.color = btnColor;
    }

    /* Sets the slot colour to transparent */
    private void SetSlotColorOnRemove()
    {
        Color btnColor = buttonImage.color;
        btnColor.a = 0;
        buttonImage.color = btnColor;
    }

    /* Inserts weapon into slot */
    private void InsertWeaponIntoSlot(Weapon weapon)
    {
        // Create new weapon object and copy details
        weaponInSlot = new Weapon();
        weaponInSlot.ReadWeapon(weapon);

        // Set button sprite image
        if(weaponInSlot.GetSpriteRenderer() != null)
        {
            buttonImage.sprite = weaponInSlot.GetSpriteRenderer().sprite;
        }
        else
        {
            buttonImage.sprite = Resources.Load<Sprite>("Weapons/" + weaponInSlot.GetImageSource());
        }
    }

    /* Inserts consumable item into slot*/
    private void InsertConsumableIntoSlot(ConsumableItem consumable)
    {
        // Create new consumable item object and copy details
        consumableInSlot = new ConsumableItem();
        consumableInSlot.Clone(consumable);
        // Set button sprite image
        if(consumableInSlot.GetSpriteRenderer() != null)
        {
            buttonImage.sprite = consumableInSlot.GetSpriteRenderer().sprite;
        }
        else
        {
            buttonImage.sprite = Resources.Load<Sprite>("Consumables/" + consumableInSlot.GetImageSource());
        }
    }

    /* Removes item from slot */
    public void RemoveItemFromSlot()
    {
        // Clear button image
        buttonImage.sprite = null;
        consumableInSlot = null;
        weaponInSlot = null;
        SetSlotColorOnRemove();
    }

    /* Get the current item in the slot */
    public Item GetItemInSlot()
    {
        if(weaponInSlot != null)
        {
            return weaponInSlot;
        }
        return consumableInSlot;
    }

    /* Checks if slot is currently empty*/
    public bool IsSlotEmpty()
    {
        if(weaponInSlot == null && consumableInSlot == null)
        {
            return true;
        }
        return false;
    }



}