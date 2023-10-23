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
    public void DamageEnemy(int damage)
    {
        currHealth -= damage;

        Debug.LogWarning("Enemey health is now >> " + currHealth);

        if (currHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Dead");
        }
    }
}