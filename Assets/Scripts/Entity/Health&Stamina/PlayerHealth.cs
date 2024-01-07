using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
/*
Health class responsible to maintain the player health 
 */
public class Health : MonoBehaviour
{

    public int currHealth = 0;
    public int maxHealth = 100;

    void Start()
    {
        // Load presaved health or define starting health if no definition present
        if(PlayerPrefs.GetInt("Health") == 0)
        {
            currHealth = maxHealth;
            PlayerPrefs.SetInt("Health", currHealth);
        }
        else
        {
            currHealth = PlayerPrefs.GetInt("Health");
        }

    }

    // Method to Damage Player
    public void DamagePlayer(int damage)
    {

        currHealth -= damage;
        // Player died
        if (currHealth <= 0)
        {
            SceneManager.LoadScene("GameOverScreen", LoadSceneMode.Single);
        }
    }

    // Method to Heal Player
    public void HealPlayer(int health)
    {
        currHealth += health;

        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
    }

    // Starts a coroutine that heals the player by health for s seconds
    public void HealPlayerOverTime(int health, float seconds)
    {
        StartCoroutine(HealOverTimeCoroutine(health, seconds));
    }

    /* Method to heal player over a determined time */
    private IEnumerator HealOverTimeCoroutine(int health, float seconds)
    {
        float timePassed = 0f;
        float healInterval = 0.5f;

        while (timePassed < seconds)
        {
            HealPlayer(health);
            yield return new WaitForSeconds(healInterval);

            timePassed += healInterval;
        }
    }
}
