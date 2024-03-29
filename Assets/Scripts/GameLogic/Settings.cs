using UnityEngine;
using System.IO;

[System.Serializable]

/* SettingsData class represents the customizable keybindings in the game*/

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


/* Settings class allows the retrieval of keybindings applicationally scoped */
public static class Settings
{

    private static SettingsData data;

    public static void LoadSettings()
    {
        TextAsset filePath = Resources.Load<TextAsset>("Keyboard");
        if(filePath == null || filePath.Equals(""))
        {
            return;
        }
        data = JsonUtility.FromJson<SettingsData>(filePath.text);

        Debug.Log("Settings Loaded");

    }

    public static SettingsData GetData()
    {

        return data;
    }



}