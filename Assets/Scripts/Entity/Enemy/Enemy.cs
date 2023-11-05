using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private ObjectPool poolInstance;

    public void Init(Vector3 pos)
    {
        transform.position = pos;
        poolInstance = ObjectPool.SharedInstance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet") && collision.gameObject.activeInHierarchy)
        {

            DecrementCurrentActiveEnemies();
            collision.gameObject.SetActive(false);
            GetComponent<EnemyHealth>().DamageEnemy(collision.gameObject.GetComponent<BulletDamage>().GetDamage());
        }
    }

    public void DecrementCurrentActiveEnemies()
    {
        poolInstance.currentActiveEnemies--;
    }

}
