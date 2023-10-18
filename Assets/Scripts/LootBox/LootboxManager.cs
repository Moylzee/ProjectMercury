using System.Collections;
using UnityEngine;

public static class LootboxManager
{
    public static GameObject lootboxPrefab;

    /*
    
        Method will create a new game object
        Create an inventory for it
        Create random weapons and or consumables
        and put them inside of the inventory

     */
    public static void CreateLootbox(Vector2 position)
    {
        GameObject lootbox = GameObject.Instantiate(lootboxPrefab, position, Quaternion.identity);

    }

}