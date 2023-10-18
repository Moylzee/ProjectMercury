using UnityEngine;



/**
 * Currently the Starting point of the Game
 * 
 */
public class Game : MonoBehaviour
{
    void Start()
    {

        Weapon weapon1 = new();
        Weapon weapon2 = new();
        Weapon weapon3 = new();
        Weapon weapon4 = new();


        Weapon weaponData = WeaponLoader.GetWeapon("AK47");
        Weapon weaponData2 = WeaponLoader.GetWeapon("AR15");
        Weapon weaponData3 = WeaponLoader.GetWeapon("FAMAS G2");
        Weapon weaponData4 = WeaponLoader.GetWeapon("AUG");

        weapon1.ReadWeapon(weaponData);
        weapon2.ReadWeapon(weaponData2);
        weapon3.ReadWeapon(weaponData3);
        weapon4.ReadWeapon(weaponData4);
        weapon1.SetOnGround(true);
        weapon2.SetOnGround(true);
        weapon3.SetOnGround(true);
        weapon4.SetOnGround(true);

        WeaponLoader.CreateWeaponObject(new Vector2(-90, 20), weapon1);
        WeaponLoader.CreateWeaponObject(new Vector2(-120, -40), weapon2);
        WeaponLoader.CreateWeaponObject(new Vector2(-100, -76), weapon3);
        WeaponLoader.CreateWeaponObject(new Vector2(-120, -76), weapon4);

        Weapon weapon5 = new();
        weapon5.ReadWeapon(WeaponLoader.GetWeapon("Astra Model 900"));
        weapon5.SetOnGround(true);
        WeaponLoader.CreateWeaponObject(new Vector2(-150, 0), weapon5);

        Weapon weapon6 = new();
        weapon6.ReadWeapon(WeaponLoader.GetWeapon("SCAR-L"));
        weapon6.SetOnGround(true);
        WeaponLoader.CreateWeaponObject(new Vector2(-150, -40), weapon6);

        Weapon weapon7 = new();
        weapon7.ReadWeapon(WeaponLoader.GetWeapon("SCAR-H"));
        weapon7.SetOnGround(true);
        WeaponLoader.CreateWeaponObject(new Vector2(-150, -55), weapon7);


        ConsumableItem item = new();
        ConsumableItem itemData = ConsumableItemLoader.GetItem("Test Heal");
        item.Clone(itemData);

        item.SetOnGround(true);

        ConsumableItemLoader.CreateConsumableItem(new Vector2(-140, -50), item);
    }
}