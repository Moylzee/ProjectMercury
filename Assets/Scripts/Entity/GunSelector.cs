using UnityEngine;
using System.Collections.Generic;

public class GunSelector : MonoBehaviour
{
    private List<GameObject> weaponList = new();

    void Start()
    {
        Weapon weapon1 = new();
        Weapon weaponData1 = WeaponLoader.GetWeapon("Astra Model 900");
        weapon1.ReadWeapon(weaponData1);
        weapon1.SetOnGround(true);

        GameObject ob1 = WeaponLoader.CreateWeaponObject(new Vector2(427.1f, 219f),  weapon1);
        ob1.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";

        Weapon weapon2 = new();
        Weapon weaponData2 = WeaponLoader.GetWeapon("AK47");
        weapon2.ReadWeapon(weaponData2);
        weapon2.SetOnGround(true);

        GameObject ob2 = WeaponLoader.CreateWeaponObject(new Vector2(520f, 219f),  weapon2);
        ob2.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";

        Weapon weapon3 = new();
        Weapon weaponData3 = WeaponLoader.GetWeapon("Thompson");
        weapon3.ReadWeapon(weaponData3);
        weapon3.SetOnGround(true);

        GameObject ob3 = WeaponLoader.CreateWeaponObject(new Vector2(615f, 219f),  weapon3);
        ob3.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";

        weaponList.Add(ob1);
        weaponList.Add(ob2);
        weaponList.Add(ob3);
    }

    void Update()
    {
        foreach(GameObject ob in weaponList)
        {
            if(ob == null) // A Weapon has been picked up
            {
                DestroyWeaponList();
                return;
            }
        }
    }

    // Destroy other weapons in weapon list once a weapon has been picked up.
    private void DestroyWeaponList()
    {
        foreach(GameObject ob in weaponList)
        {
            if(ob != null)
            {
                Destroy(ob);
            }
            
        }
    }
}
