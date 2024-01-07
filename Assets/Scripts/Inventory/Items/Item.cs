
using UnityEngine;
using System;

/*
 * Item Abstract Class, represents an item in the game
 * and decribes which attributes it needs
 * Implements Accessor methods for all items
 */
public abstract class Item
{
    // Attributes
    public string UID { get; set; }

    public string ItemName;
    public string ImageSource;
    public int Rarity;
    public string Tooltip;
    private bool PickedUp = false;
    private bool OnGround = false; // If the item is on the ground this should be true
    private Vector2 offset;



    private SpriteRenderer spriteRenderer;

    public Item()
    {

    }


    // Creates a new random Unique ID for item
    protected void SetUID()
    {
        this.UID = Guid.NewGuid().ToString();
    }

    protected void SetUID(string id)
    {
        this.UID = id;
    }


    public string GetTooltip()
    {
        return Tooltip;
    }

    public void SetTooltip(string Tooltip)
    {
        this.Tooltip = Tooltip;
    }

    public int GetItemRarity()
    {
        return Rarity;
    }

    public void SetItemRarity(int Rarity)
    {
        this.Rarity = Rarity;
    }

    public bool IsOnGround()
    {
        return OnGround;
    }


    public void SetOnGround(bool onGround)
    {
        OnGround = onGround;
    }

    public string GetItemName()
    {
        return ItemName;
    }
    public string GetImageSource()
    {
        return ImageSource;
    }

    public bool IsPickedUp()
    {
        return PickedUp;
    }


    public void TogglePickedUp()
    {
        PickedUp = !PickedUp;
    }

    public void SetPickedUp(bool pickedUp)
    {
        this.PickedUp = pickedUp;
    }

    public void SetItemName(string name)
    {
        this.ItemName = name;
    }

    public void SetImageSource(string src)
    {
        this.ImageSource = src;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }

    public void SetSpriteRenderer(SpriteRenderer sprite)
    {
        this.spriteRenderer = sprite;
    }

    public void SetOffset(Vector2 vec)
    {
        offset = vec;
    }

    public Vector2 GetOffset()
    {
        return offset;
    }


}
