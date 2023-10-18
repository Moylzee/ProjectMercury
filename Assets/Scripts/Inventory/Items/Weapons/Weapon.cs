using UnityEngine;
using System.Collections.Generic;
using System;

// Used to load Weapons from Json file
[System.Serializable]
public class WeaponList
{
    public Weapon[] weapons;
}


[System.Serializable]
public class Weapon : Item
{
    public ushort MagazineSize;
    public uint DamageDealtPerBullet;
    public uint SpareAmmo;
    public string WeaponCategory;

    private ushort BulletsInMag;


    public Weapon()
    {

    }

    public void ReadWeapon(Weapon weaponData)
    {
        setUID(); // Creates a brand new UID for the item
        SetItemName(weaponData.GetItemName());
        SetImageSource(weaponData.GetImageSource());
        SetSpareAmmo(weaponData.GetSpareAmmo());
        SetWeaponCategory(weaponData.GetWeaponCategory());
        SetDamageDealtPerBullet(weaponData.GetDamageDealtPerBullet());
        SetMagazineSize(weaponData.GetMagazineSize());

        SetBulletsInMag(weaponData.GetMagazineSize());
    }

    // Getters and Setters

    public void SetBulletsInMag(ushort mag)
    {
        BulletsInMag = mag;
    }

    public ushort GetBulletsInMag()
    {
        return BulletsInMag;
    }

    public ushort GetMagazineSize()
    {
        return MagazineSize;
    }

    public uint GetDamageDealtPerBullet()
    {
        return DamageDealtPerBullet;
    }

    public uint GetSpareAmmo()
    {
        return SpareAmmo;
    }

    public string GetWeaponCategory()
    {
        return WeaponCategory;
    }

    public void SetMagazineSize(ushort magazineSize)
    {
        this.MagazineSize = magazineSize;
    }

    public void SetDamageDealtPerBullet(uint damage)
    {
        this.DamageDealtPerBullet = damage;
    }

    public void SetSpareAmmo(uint spareAmmo)
    {
        this.SpareAmmo = spareAmmo;
    }

    public void SetWeaponCategory(string category)
    {
        this.WeaponCategory = category;
    }

    public static Weapon CreateFromJson(string jsonString)
    {
        return JsonUtility.FromJson<Weapon>(jsonString);
    }


}
