using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currHealth = 0;
    public int maxHealth = 30;

    void Start()
    {
        currHealth = maxHealth;
    }

    public void SetHealth(int health)
    {
        this.currHealth = health;
    }

    public void DamageEnemy(int damage)
    {
        currHealth -= damage;

        if (currHealth <= 0)
        {
            GetComponent<Enemy>().DecrementCurrentActiveEnemies();
            gameObject.SetActive(false);

            return;

        }
    }
}