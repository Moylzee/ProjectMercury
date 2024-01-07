using UnityEngine;

public static class LootboxManager
{
    public static GameObject lootboxPrefab;
    
    /* Method to create loot box*/
    public static void CreateLootbox(Vector2 position)
    {
        GameObject lootbox = GameObject.Instantiate(lootboxPrefab, position, Quaternion.identity);

    }

}