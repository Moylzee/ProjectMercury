using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currHealth = 0;
    public int maxHealth = 30;

    public List<GameObject> spawnList = new();

    void Start()
    {
        currHealth = maxHealth;
    }
    public void DamageEnemy(int damage)
    {
        currHealth -= damage;

        if (currHealth <= 0)
        {
            spawnList.Remove(gameObject);
            Destroy(gameObject);

        }
    }
}