
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

}


public static class WeaponLoader
{

    public static WeaponList weaponList;

    public static bool WeaponsRead = false;
    private static Dictionary<string, Weapon> weaponDictionary = new();

    public static GameObject weaponPrefab;

    private static float ItemSize = 15f;

    public static void LoadPrefab(GameObject prefab)
    {
        weaponPrefab = prefab;
    }

    public static void LoadWeaponsFromFile()
    {
        Debug.Log("Loading weapons from file....");
        if (WeaponsRead)
        {
            Debug.LogWarning("Attempted reading of Weapon File twice");
            return;
        }
        string jsonFile = File.ReadAllText("Assets/GameData/Weapons/WeaponData.json");
        weaponList = JsonUtility.FromJson<WeaponList>(jsonFile);

        foreach (Weapon weapon in weaponList.weapons)
        {
            AddWeapon(weapon);
        }

        WeaponsRead = true;
        Debug.Log("Finished loading weapons from file!");
    }


    /**
     * Method creates a GameObject using the weapon data provided and spawns it at the vector2 
     * The object has a WeaponObjectBehaviour component, which controls how the weapon behaves
     * The object has a sprite renderer component, which allows it to be visible
     */
    public static GameObject CreateWeaponObject(Vector2 position, Weapon weaponData)
    {
        GameObject obj = GameObject.Instantiate(weaponPrefab, position, Quaternion.identity);
        WeaponObjectBehaviour weaponObjectBehaviour = obj.GetComponent<WeaponObjectBehaviour>();

        SpriteRenderer s_renderer = obj.AddComponent<SpriteRenderer>();
        weaponData.SetSpriteRenderer(s_renderer);
        weaponObjectBehaviour.item = weaponData;
        weaponObjectBehaviour.item.ItemType = "Weapon";

        // Sets sprite image, and places in front of Map
        s_renderer.sprite = Resources.Load<Sprite>("Weapons/" + weaponData.GetImageSource()) as Sprite;
        s_renderer.sortingOrder = 3;

        // Sets scale
        obj.transform.localScale = new Vector2(ItemSize, ItemSize);

        // Sets all the game objects properties
        if (weaponObjectBehaviour != null)
        {
            return obj;
        }

        Debug.LogError("WeaponObjectBehavior is null");
        return null;
    }

    // Adds weapon to dictionary
    static void AddWeapon(Weapon weapon)
    {
        weaponDictionary.Add(weapon.GetItemName(), weapon);
    }

    // Retrieves Weapon from dictionary
    public static Weapon GetWeapon(string itemName)
    {
        if (weaponDictionary.Count == 0)
        {
            Debug.LogError("Weapon dictionary is empty");
            return null;
        }

        if (weaponDictionary.TryGetValue(itemName, out Weapon weapon))
        {
            return weapon;
        }
        else
        {
            Debug.LogError("Item not found in Weapon Dictionary >> WeaponManager");
            return null;
        }
    }

    public static Weapon GetRandomWeapon()
    {
        int RandomIndex = Random.Range(0, weaponList.weapons.Length);
        return weaponList.weapons[RandomIndex];
    }

    public static Weapon GetRandomWeapon_Bias()
    {
        int RandomRange = Random.Range(0, weaponList.weapons.Length);
        // Formula for Spawn chance in lootboxes
        // R > N / Rarity * M, where R is a random number, N is a float between 0.1 and 0.9, M is a multiplier
        // The higher the M the lower the chances of something spawning
        // R > 0.6f / Rarity * 4.2
        if (Random.Range(0f, 1f) > 0.6f / (float)weaponList.weapons[RandomRange].GetItemRarity() * 4.2f)
        {
            return weaponList.weapons[RandomRange];
        }

        return null;
    }

}