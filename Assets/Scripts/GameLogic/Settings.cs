
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

    public string GetKey_WeaponSlot1()
    {

        if (weaponSlot1 == null)
        {
            // Setting default
            Debug.Log("GetKey WPS 1: set to default key!");
            weaponSlot1 = "1";
        }

        return weaponSlot1;
    }

    public string GetKey_WeaponSlot2()
    {

        if (weaponSlot2 == null)
        {
            // Setting default
            Debug.Log("GetKey WPS 2: set to default key!");
            weaponSlot1 = "2";
        }

        return weaponSlot2;
    }

    public string GetKey_ItemSlot1()
    {

        if (itemSlot1 == null)
        {
            // Setting default
            Debug.Log("GetKey IS1 : set to default key!");
            itemSlot1 = "1";
        }

        return itemSlot1;
    }

    public string GetKey_ItemSlot2()
    {

        if (itemSlot2 == null)
        {
            // Setting default
            Debug.Log("GetKey IS2 : set to default key!");
            itemSlot2 = "1";
        }

        return itemSlot2;
    }

    public string GetKey_ItemSlot3()
    {

        if (itemSlot3 == null)
        {
            // Setting default
            Debug.Log("GetKey IS3 : set to default key!");
            itemSlot3 = "1";
        }

        return itemSlot3;
    }

    public string GetKey_InteractKey()
    {
        if (interactKey == null)
        {
            Debug.Log("GetKey IK : set to default key!");
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
            Debug.Log("Wtf");
            return;
        }
        else
        {
            Debug.Log(filePath);
        }
        data = JsonUtility.FromJson<SettingsData>(filePath);

        Debug.Log("Settings Loaded");

    }

    public static SettingsData GetData()
    {
        if (data == null)
        {
            Debug.Log("data is null");
        }
        return data;
    }



}