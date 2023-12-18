
using UnityEngine;
using System;

public abstract class Item
{

    public string UID { get; set; }

    public string ItemName;
    public string ImageSource;
    public int Rarity;

    private bool PickedUp = false;
    private bool OnGround = false; // If the weapon is on the ground this should be true
    private bool InSlot = false;
    private Vector2 offset;
    private bool Dragging = false;



    private SpriteRenderer spriteRenderer;

    public Item()
    {

    }

    public Item DeepCopyWeapon(Weapon weapon)
    {
        Weapon copiedWeapon = new Weapon();
        copiedWeapon.ReadWeapon(weapon);
        return copiedWeapon;
    }

    
    protected void setUID()
    {
        this.UID = Guid.NewGuid().ToString();
    }


    public int GetItemRarity()
    {
        return Rarity;
    }

    public void SetItemRarity(int Rarity)
    {
        this.Rarity = Rarity;
    }

    public bool IsDragging()
    {
        return Dragging;
    }

    public void SetDragging(bool dragging)
    {
        this.Dragging = dragging;
    }

    public bool IsInSlot()
    {
        return InSlot;
    }


    public void SetInSlot(bool inSlot)
    {
        InSlot = inSlot;
    }

    public bool IsOnGround()
    {
        return OnGround;
    }

    public void ToggleOnGround()
    {
        OnGround = !OnGround;
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

public class ItemObject : MonoBehaviour
{

}