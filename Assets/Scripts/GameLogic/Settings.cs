
using UnityEngine;
using System.IO;

[System.Serializable]
public class SettingsData
{

    public string weaponSlot1;
    public string weaponSlot2;
    public string itemSlot1;
    public string itemSlot2;
    public string itemSlot3;
    public string interactKey;
    public string reloadWeapon;

    public string GetKey_ReloadWeapon()
    {
        if(reloadWeapon == null)
        {
            reloadWeapon = "r";
        }
        return reloadWeapon;
    }

    public string GetKey_WeaponSlot1()
    {

        if (weaponSlot1 == null)
        {
            // Setting default
            weaponSlot1 = "1";
        }

        return weaponSlot1;
    }

    public string GetKey_WeaponSlot2()
    {

        if (weaponSlot2 == null)
        {
            // Setting default
            weaponSlot1 = "2";
        }

        return weaponSlot2;
    }

    public string GetKey_ItemSlot1()
    {

        if (itemSlot1 == null)
        {
            // Setting default
            itemSlot1 = "3";
        }

        return itemSlot1;
    }

    public string GetKey_ItemSlot2()
    {

        if (itemSlot2 == null)
        {
            // Setting default
            itemSlot2 = "4";
        }

        return itemSlot2;
    }

    public string GetKey_ItemSlot3()
    {

        if (itemSlot3 == null)
        {
            // Setting default
            itemSlot3 = "5";
        }

        return itemSlot3;
    }

    public string GetKey_InteractKey()
    {
        if (interactKey == null)
        {
            interactKey = "e";
        }

        return interactKey;

    }

}



public static class Settings
{

    private static SettingsData data;

    public static void LoadSettings()
    {
        string filePath = File.ReadAllText("Assets/GameData/GameSettings/Keyboard.json");
        if(filePath == null || filePath.Equals(""))
        {
            return;
        }
        data = JsonUtility.FromJson<SettingsData>(filePath);

        Debug.Log("Settings Loaded");

    }

    public static SettingsData GetData()
    {

        return data;
    }



}