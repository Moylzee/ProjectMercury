using UnityEngine;

/*
 * AmmoBoostItem class represents the Ammo Boost object in game as an Item bonus
 */
public class AmmoBoostItem : ItemBonus
{

    private readonly PlayerObject playerObject;
    private readonly ushort spareAmmoIncrease = 100;



    public AmmoBoostItem()
    {
        playerObject = GameObject.FindWithTag("Player").GetComponent<PlayerObject>();

        // Set item Attributes
        SetItemName("Ammo Boost");
        SetImageSource("maxammo");
        SetTooltip("Boosts ammo for the equipped weapon");
    }

    /* UseEffect method applies the items effect to player*/
    public override void UseEffect()
    {
        var spareAmmo = playerObject.Inventory.GetWeaponSpareAmmoBasedOnCategory();
        PlayerInventory.SetWeaponSpareAmmoBasedOnCategory(playerObject.Inventory, spareAmmo + spareAmmoIncrease);

        playerObject.Inventory.UpdateDetails();
    }

}