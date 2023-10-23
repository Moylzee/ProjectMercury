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
            Debug.Log("Bullet hit Enemy");
            var healthComponent = enemy.GetComponent<EnemyHealth>();
            if (healthComponent != null)
            {
                Debug.Log("Damaged Enemey by " + damage);
                healthComponent.DamageEnemy(damage);
            }
        }
    }
}