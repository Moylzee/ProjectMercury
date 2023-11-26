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
        }

        // Increase player score when enemy damaged
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") 
            + PlayerPrefs.GetInt("ENEMY_DAMAGE_POINTS"));


        playerScore.UpdatePointText();
    }
}