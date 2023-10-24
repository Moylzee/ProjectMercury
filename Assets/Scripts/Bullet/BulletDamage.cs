using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    private Enemy enemy;
    private int damage = 10;

    void Start()
    {
        enemy = FindObjectOfType<Enemy>();
    }

    void Update()
    {

    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }
    
    public int GetDamage()
    {
        return this.damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            var healthComponent = enemy.GetComponent<EnemyHealth>();
            if (healthComponent != null)
            {
                healthComponent.DamageEnemy(damage);
            }
        }
    }
}