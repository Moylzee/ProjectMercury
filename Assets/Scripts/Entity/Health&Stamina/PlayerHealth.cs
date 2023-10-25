using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int currHealth = 0;
    public int maxHealth = 100;
    public PauseMenu EndGameManager;

    void Start()
    {
        currHealth = maxHealth;
    }

    // Method to Damage Player
    public void DamagePlayer(int damage)
    {
        currHealth -= damage;

        if (currHealth <= 0)
        {
            EndGameManager.GameOver();
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
