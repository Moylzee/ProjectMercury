using UnityEngine;

/**
 * GameLoader class is attached to the first gameObject in the hierarchy and is the first thing to be loaded 
 *  contains Settings [Keyboard, User Preferences, etc]
 *  contains Weapon and Consumable object deserialization
 */
public class GameLoader : MonoBehaviour
{


    public GameObject ConsumableItemPrefab;
    public GameObject WeaponItemPrefab;

    
    //Entry point in the game
    void Awake()
    {
        Settings.LoadSettings(); // Load all settigns into game

        WeaponLoader.LoadPrefab(this.WeaponItemPrefab);
        WeaponLoader.LoadWeaponsFromFile();

        ConsumableItemLoader.LoadPrefab(this.ConsumableItemPrefab);
        ConsumableItemLoader.LoadConsumableItemsFromFile();


    }
    
}