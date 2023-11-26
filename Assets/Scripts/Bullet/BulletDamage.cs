using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;


/*
 * Bullet Damage Script
 * Script responsible for damanging entities (i.e. Enemeies)
 * and deactivating the bullet game object upon collision
 */

public class BulletDamage : MonoBehaviour
{

    // Damage per bullet, with default value
    private int damage = 10;


    /* On Collision, disable object, and damage enemy*/
    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch (collision.gameObject.tag)
        {
            case "Enemy":
                gameObject.SetActive(false);
                var healthComponent = collision.gameObject.GetComponent<EnemyHealth>();
                if (healthComponent != null)
                {
                    // Transfer the damage to the enemy
                    healthComponent.DamageEnemy(damage);
                }
                gameObject.SetActive(false);
                break;
            case "Collision":
                gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    // Accessor methods

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    public int GetDamage()
    {
        return this.damage;
    }
}