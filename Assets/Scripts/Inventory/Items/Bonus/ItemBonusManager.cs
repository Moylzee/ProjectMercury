using System.Collections;
using UnityEngine;

public static class ItemBonusLoader
{

    public static GameObject itemPrefab;
    private static readonly float itemSize = 15f;

    public static void LoadPrefab(GameObject prefab)
    {
        itemPrefab = prefab;
    }


    public static GameObject CreateItemBonusObject(Vector2 position, ItemBonus item)
    {
        GameObject obj = GameObject.Instantiate(itemPrefab, position, Quaternion.identity);
        ItemBonusObjectBehaviour itemBonusObjectBehaviour = obj.GetComponent<ItemBonusObjectBehaviour>();

        item.SetSpriteRenderer(itemPrefab.GetComponent<SpriteRenderer>());
        itemBonusObjectBehaviour.item = item;

        item.GetSpriteRenderer().sprite = Resources.Load<Sprite>("Misc/EnemyDrops/" + item.GetImageSource());
        item.GetSpriteRenderer().sortingOrder = 3;

        obj.transform.localScale = new Vector2(itemSize, itemSize);
        if(itemBonusObjectBehaviour != null)
        {
            return obj;
        }

        Debug.LogError("itemBonusObjectBehaviour is null");
        return null;


    }

}
