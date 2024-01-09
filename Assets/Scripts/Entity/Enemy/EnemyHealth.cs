using UnityEngine;

/*
 * EnemyHealth Script
 * Script controls enemy health 
 */

public class EnemyHealth : MonoBehaviour
{
    // Enemy health attributes
    public int currHealth = 0;
    public int maxHealth = 30;

    private PlayerScore playerScore;

    void Start()
    {
        currHealth = maxHealth;
        playerScore = GameObject.FindWithTag("Player").GetComponent<PlayerScore>();
    }

    public void SetHealth(int health)
    {
        this.currHealth = health;
    }

    /* Method to damage enemy, controls player score incrementations */
    public void DamageEnemy(int damage)
    {
        currHealth -= damage;


        // Enemy dies
        if (currHealth <= 0)
        {
            GetComponent<Enemy>().DecrementCurrentActiveEnemies();
            gameObject.SetActive(false);

            // Increase player score when enemy gets killed, and update display
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score")
                + PlayerPrefs.GetInt("ENEMY_KILL_POINTS"));

            PlayerPrefs.SetInt("ZombiesKilled", PlayerPrefs.GetInt("ZombiesKilled") + 1);

            DropRandomChance();

        }

        // Increase player score when enemy damaged
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") 
            + PlayerPrefs.GetInt("ENEMY_DAMAGE_POINTS"));


        playerScore.UpdatePointText();

        PlayerPrefs.Save();
    }

    /* Generates a random chance to drop something */
    void DropRandomChance()
    {
        
        if(Random.Range(1, 100) < 5)
        {
            int dropType = Random.Range(0, 3);

            switch (dropType)
            {
                case 0:
                    // First case: spawns random weapon on the ground
                    Weapon random1 = WeaponLoader.GetRandomWeapon();
                    

                    Weapon weaponCopy = new();
                    weaponCopy.ReadWeapon(random1);
                    weaponCopy.SetOnGround(true);
                    WeaponLoader.CreateWeaponObject(transform.position, weaponCopy);
                    break;
                case 1:
                    // Second case: spawns random consumable item on the ground
                    ConsumableItem random2 = ConsumableItemLoader.GetRandomItem();
                    random2.SetOnGround(true);
                    ConsumableItemLoader.CreateConsumableItem(transform.position, random2);
                    break;
                case 2:
                    // Third case: spawns an ammo boost on the ground
                    ItemBonus random3 = new AmmoBoostItem();
                    ItemBonusLoader.CreateItemBonusObject(transform.position, random3);
                    break;

            }
        }
    }

}