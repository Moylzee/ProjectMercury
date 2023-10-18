
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
public abstract class ItemObjectBehaviour<T> : MonoBehaviour where T : Item, new()
{

    public T item;

    public bool PlayerNearby = false;
    public bool Moved = false;

    protected PlayerObject PlayerScript;

    void Start()
    {
        PlayerScript = GameObject.FindWithTag("Player").GetComponent<PlayerObject>();
    }

    public virtual void Update()
    {

    }

    public void BaseUpdate()
    {

    }


    public virtual void KeyboardHandler() { }
    public virtual void InventoryLogic() { }

    // Setters and Getters
    public void SetItemName(string itemName)
    {
        item.SetItemName(itemName);
    }

    public void SetImageSource(string imageSource)
    {
        item.SetImageSource(imageSource);
    }



    public void SetSpriteRenderer(SpriteRenderer spriteRenderer)
    {
        item.SetSpriteRenderer(spriteRenderer);
    }

    public string GetItemName()
    {
        return item.GetItemName();
    }
    public string GetImageSource()
    {
        return item.GetImageSource();
    }

    public void SetOnGround(bool onGround)
    {
        item.SetOnGround(onGround);
    }

    public bool IsOnGround()
    {
        return item.IsOnGround();
    }

    public bool IsPickedUp()
    {
        return item.IsPickedUp();
    }

    public void SetPickedUp(bool pickedUp)
    {
        item.SetPickedUp(pickedUp);
    }

    public void TogglePickedUp()
    {
        item.TogglePickedUp();
    }


}
