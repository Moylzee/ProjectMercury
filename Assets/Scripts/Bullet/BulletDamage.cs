using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch (collision.gameObject.tag)
        {
            case "Enemy":
                Destroy(gameObject);
                var healthComponent = collision.gameObject.GetComponent<EnemyHealth>();
                if (healthComponent != null)
                {
                    healthComponent.DamageEnemy(damage);
                }
                break;
            case "Collision":
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}