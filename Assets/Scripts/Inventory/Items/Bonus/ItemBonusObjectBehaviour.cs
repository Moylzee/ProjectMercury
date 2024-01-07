
/* ItemBonusObjectBehaviour allows gameobjects to use Item behaviours */
public class ItemBonusObjectBehaviour : ItemObjectBehaviour<ItemBonus>
{
    public override void Update()
    {
        this.BaseUpdate();
        this.InventoryLogic();
    }
    public override void InventoryLogic()
    {
        // If item is picked up use effect
        if (item.IsPickedUp())
        {
            this.item.UseEffect();
        }
    }

}