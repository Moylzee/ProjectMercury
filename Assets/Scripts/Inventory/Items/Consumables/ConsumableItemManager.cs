
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConsumableItemManager : MonoBehaviour
{

}

public static class ConsumableItemLoader
{
    public static ConsumableItemList consumableList;
    public static bool ConsumableItemsRead = false;

    public static Dictionary<string, ConsumableItem> consumableDictionary = new();
    public static GameObject itemPrefab;
    public static float ItemSize = 15f;

    public static void LoadPrefab(GameObject prefab)
    {
        itemPrefab = prefab;
    }

    public static void LoadConsumableItemsFromFile()
    {
        Debug.Log("Loading consumable items from file...");
        if (ConsumableItemsRead)
        {
            Debug.LogWarning("Attempted reading of Consumable Items file twice");
        }
        string jsonFile = File.ReadAllText("Assets/GameData/Consumables/ConsumableData.json");
        consumableList = JsonUtility.FromJson<ConsumableItemList>(jsonFile);

        foreach (ConsumableItem item in consumableList.consumables)
        {
            AddItem(item);
        }

        ConsumableItemsRead = true;
        Debug.Log("Finished loading consumable items from file!");

    }


    public static GameObject CreateConsumableItem(Vector2 position, ConsumableItem itemData)
    {
        GameObject obj = GameObject.Instantiate(itemPrefab, position, Quaternion.identity);
        ConsumableItemObjectBehaviour consumableItemBehaviour = obj.GetComponent<ConsumableItemObjectBehaviour>();
        SpriteRenderer s_renderer = obj.AddComponent<SpriteRenderer>();
        itemData.SetSpriteRenderer(s_renderer);
        consumableItemBehaviour.item = itemData;
        consumableItemBehaviour.item.ItemType = "Consumable";

        // Sets sprite image, and places in front of Map
        s_renderer.sprite = Resources.Load<Sprite>("Consumables/" + itemData.GetImageSource()) as Sprite;
        s_renderer.sortingOrder = 3;

        // Sets scale
        obj.transform.localScale = new Vector2(8f, 8f);

        if (consumableItemBehaviour != null)
        {
            return obj;
        }

        Debug.LogError("consumableItemBehaviour is null");
        return null;
    }


    public static void AddItem(ConsumableItem item)
    {
        consumableDictionary.Add(item.GetItemName(), item);
    }

    public static ConsumableItem GetItem(string itemName)
    {
        if (consumableDictionary.TryGetValue(itemName, out ConsumableItem item))
        {
            return item;
        }
        else
        {
            Debug.LogError("Item not foind in Consumable Dictionary >> ConsumableItemManger");
            return null;
        }
    }

    public static ConsumableItem GetRandomItem()
    {
        int RandomIndex = Random.Range(0, consumableList.consumables.Length);
        return consumableList.consumables[RandomIndex];
    }

    public static ConsumableItem GetRandomItem_Bias()
    {
        int RandomRange = Random.Range(0, consumableList.consumables.Length);
        // Formula for Spawn chance in lootboxes
        // R > N / Rarity * M, where R is a random number, N is a float between 0.1 and 0.9, M is a multiplier
        // The higher the M the lower the chances of something spawning
        // R > 0.6f / Rarity * 4.2
        if (Random.Range(0f, 1f) > 0.5f)
        {
            return consumableList.consumables[RandomRange];
        }

        return null;
    }

}