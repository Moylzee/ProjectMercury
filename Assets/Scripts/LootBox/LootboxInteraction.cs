using UnityEngine;
using UnityEngine.UI;

public class LootboxInteraction : MonoBehaviour
{
    public GameObject InventoryPrefab;

    private GameObject BoxInventory;

    private GameObject Player;


    private bool Opened = false;

    private RarityClass Rarity; // 0 - 1, How good is a given box

    private GameObject InventorySlots;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        transform.SetParent(GameObject.FindWithTag("LootboxParent").transform); // set parent
        BoxInventory = GameObject.Instantiate(InventoryPrefab); // Create inventory prefab

        BoxInventory.transform.SetParent(GameObject.Find("Canvas").transform, false);

        BoxInventory.SetActive(false); // Disable box inventory until player opens it

        Rarity = RaritySelection(); // Rarity of the lootbox

    }

    void Update()
    {
        if (BoxInventory.activeSelf)
        {
            if (Vector2.Distance(transform.position, Player.transform.position) > Player.GetComponent<PlayerObject>().RANGE_OF_PICKUP * 1.8f)
            {
                CloseBox();
            }
        }
    }

    /*
     * Randomly selects a number from 0 - 1, 
     */
    private RarityClass RaritySelection()
    {
        float number = Random.Range(0f, 1f);
        if (number < 0.5)
        {
            return RarityClass.COMMON;
        }
        else if (number < 0.75)
        {
            return RarityClass.UNCOMMON;
        }
        else if (number < 0.92)
        {
            return RarityClass.RARE;
        }
        return RarityClass.LEGENDARY;
    }

    void FillBox()
    {

    }

    public void OpenBox()
    {
        if(!Opened)
        {
            LoadInventorySlots();
            Opened = true;
        }

        BoxInventory.SetActive(true);
    }

    public void CloseBox()
    {
        BoxInventory.SetActive(false);
        Player.GetComponent<PlayerObject>().BOX_OPEN = false;
    }


    void LoadInventorySlots()
    {
        Transform container = BoxInventory.transform.Find("ItemsBackpanel");
        if (container == null)
        {
            Debug.LogError("Lootbox Inventory is empty");
            return;
        }

        ;
        var Item1 = container.Find("Slot1").Find("Item1").GetComponent<SlotItem>();
        var Item2 = container.Find("Slot2").Find("Item2").GetComponent<SlotItem>();
        var Item3 = container.Find("Slot3").Find("Item3").GetComponent<SlotItem>();
        var Item4 = container.Find("Slot4").Find("Item4").GetComponent<SlotItem>();

        Weapon weapon = new();
        Weapon random = WeaponLoader.GetRandomWeapon_Bias();
        if (random != null)
        {
            weapon.ReadWeapon(random);
            GameObject go = WeaponLoader.CreateWeaponObject(new Vector2(-1000, -1000), weapon);
            Item1.InsertItemInSlot(weapon);
            Destroy(go);
        }

        Weapon weapon2 = new();
        random = WeaponLoader.GetRandomWeapon_Bias();
        if (random != null)
        {
            weapon2.ReadWeapon(random);
            GameObject go = WeaponLoader.CreateWeaponObject(new Vector2(-1000, -1000), weapon2);
            Item2.InsertItemInSlot(weapon2);
            Destroy(go);
        }

        Weapon weapon3 = new();
        random = WeaponLoader.GetRandomWeapon_Bias();
        if (random != null)
        {
            weapon3.ReadWeapon(random);
            GameObject go = WeaponLoader.CreateWeaponObject(new Vector2(-1000, -1000), weapon3);
            Item3.InsertItemInSlot(weapon3);
            Destroy(go);
        }

        Weapon weapon4 = new();
        random = WeaponLoader.GetRandomWeapon_Bias();
        if (random != null)
        {
            weapon4.ReadWeapon(random);
            GameObject go = WeaponLoader.CreateWeaponObject(new Vector2(-1000, -1000), weapon4);
            Item4.InsertItemInSlot(weapon4);
            Destroy(go);
        }


    }

}


public enum RarityClass
{
    COMMON, UNCOMMON, RARE, LEGENDARY
}