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

        switch (collision.gameObject.tag)
        {
            case "Enemy":
                gameObject.SetActive(false);
                var healthComponent = collision.gameObject.GetComponent<EnemyHealth>();
                if (healthComponent != null)
                {
                    healthComponent.DamageEnemy(damage);
                }
                break;
            case "Collision":
                gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}