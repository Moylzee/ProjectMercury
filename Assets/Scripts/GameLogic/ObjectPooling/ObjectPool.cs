using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects_Bullet;
    public List<GameObject> pooledObjects_Enemy;
    public GameObject objectToPool_Bullet;
    public GameObject objectToPool_Enemy;
    public int amountToPool_Bullet;
    public int amountToPool_Enemy;

    public int ENEMY_LIMIT;
    public int currentActiveEnemies = 0;

    void Awake()
    {
        SharedInstance = this;
        ENEMY_LIMIT = amountToPool_Enemy;
    }

    void Start()
    {
        pooledObjects_Bullet = new List<GameObject>();
        pooledObjects_Enemy = new List<GameObject>();
        GameObject temp;
        for(int i = 0; i< amountToPool_Bullet; i++)
        {
            temp = Instantiate(objectToPool_Bullet);
            temp.SetActive(false);
            pooledObjects_Bullet.Add(temp);
        }
        for (int i = 0; i < amountToPool_Enemy; i++)
        {
            temp = Instantiate(objectToPool_Enemy);
            temp.SetActive(false);
            pooledObjects_Enemy.Add(temp);
        }
        
    }

    public GameObject GetPooledObjectEnemy()
    {
        for(int i = 0; i<amountToPool_Enemy; i++)
        {
            if (!pooledObjects_Enemy[i].activeInHierarchy)
            {
                currentActiveEnemies++;
                return pooledObjects_Enemy[i];
            }
        }
        Debug.LogWarning("Attempt to spawn more objects than pooling allows. Increase Pool count");
        return null;
    }


    public GameObject GetPooledObjectBullet()
    {
        for (int i = 0; i < amountToPool_Bullet; i++)
        {
            if(!pooledObjects_Bullet[i].activeInHierarchy)
            {
                return pooledObjects_Bullet[i];
            }
        }
        Debug.LogWarning("Attempt to spawn more objects than pooling allows. Increase Pool count");
        return null;
    }


}