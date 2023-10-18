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
            Debug.Log("Dead");
            EndGameManager.GameOver();
        }
    }

    // Method to Heal Player
    public void HealPlayer(int health)
    {
        currHealth += health;

        if (currHealth > maxHealth)
        {
            Debug.Log("fully healed");
            currHealth = maxHealth;
        }
    }
}
