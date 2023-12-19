using System.Collections;
using UnityEngine;

/*
 * ItemBonus class represents bonus items in the game,
 * These will not be able to be picked up and will be used on "pickup"
 */
public class ItemBonus : Item
{

    

    public ItemBonus()
    {
        // Force item to be on ground
        this.SetOnGround(true);
        this.SetPickedUp(false);
    }



    /* Overrideable function to create object effects */
    public virtual void UseEffect()
    {

    }
    

}