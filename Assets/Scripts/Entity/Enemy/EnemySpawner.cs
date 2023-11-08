using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab with the spawn animation.
    public Tilemap tilemap;
    private int Level = 20;
    public GameObject Player;

    private int EnemyMaxHealth = 30;

    public int ENEMEY_LIMIT_WAVE;

    private int EnemiesLeftToSpawnInLevel = 0;
    private ObjectPool ObjectPoolInstance;
    private float EnemeySpawnDelay = 2f;

    void Start()
    {
        ObjectPoolInstance = ObjectPool.SharedInstance;
        ENEMEY_LIMIT_WAVE = ObjectPoolInstance.ENEMY_LIMIT;
        CalculateEnemiesToSpawn();
        Debug.LogWarning(EnemiesLeftToSpawnInLevel);
        InvokeRepeating("CheckEnemies", EnemeySpawnDelay, 5f);

    }

    void CheckEnemies()
    {
        if(ObjectPoolInstance.currentActiveEnemies <= 0 && GetEnemiesLeftToSpawn() > 0)
        {
            SpawnEnemies();

        }else if(ObjectPoolInstance.currentActiveEnemies <= 0 && GetEnemiesLeftToSpawn() == 0)
        {
            Level++;
            Debug.Log("New Round! Round: " + Level);
            CalculateEnemiesToSpawn();
            CalculateEnemiesHealth();
            SpawnEnemies();
        }else if(ObjectPoolInstance.currentActiveEnemies < ENEMEY_LIMIT_WAVE && GetEnemiesLeftToSpawn() > 0)
        {
            SpawnEnemies();
        }

        Invoke("EnableEnemyAI", 1.5f);

    }


    void DecreaseEnemiesLeftToSpawn()
    {
        this.EnemiesLeftToSpawnInLevel--;
    }

    int GetEnemiesLeftToSpawn()
    {
        return this.EnemiesLeftToSpawnInLevel;
    }

    /* Function to calculate new health for the enemy after a new round */
    void CalculateEnemiesHealth()
    {
        this.EnemyMaxHealth = (int)(Mathf.Pow(2.4f, Mathf.Log(Level)) + 30);
    }

    /* Function to calculate how many enemies per round */
    void CalculateEnemiesToSpawn()
    {

        if(Level < 20)
        {
            this.EnemiesLeftToSpawnInLevel = 5 + (int)Mathf.Pow(6, Mathf.Log10(Level));
            return;
        }else if(Level >= 80)
        {
            this.EnemiesLeftToSpawnInLevel = 5 * ObjectPoolInstance.amountToPool_Enemy; // 3 waves
            return;
        }
        else
        {
            this.EnemiesLeftToSpawnInLevel = (int)(Mathf.Pow(4, Mathf.Log10(Level*10)) + 11);
        }
    }

    /* Function to spawn enemies*/
    void SpawnEnemies()
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int tilePosition in bounds.allPositionsWithin)
        {

            if (ObjectPoolInstance.currentActiveEnemies >= ENEMEY_LIMIT_WAVE || GetEnemiesLeftToSpawn() <= 0)
            {
                return;
            }

            TileBase tile = tilemap.GetTile(tilePosition);

            if (tile != null && Player.GetComponent<CircleCollider2D>().OverlapPoint(tilemap.GetCellCenterWorld(tilePosition)))
            {
                Vector3 spawnPosition = tilemap.GetCellCenterWorld(tilePosition);

                GameObject spawnedEnemy = ObjectPoolInstance.GetPooledObjectEnemy();
               
                if(spawnedEnemy == null)
                {
                    return;
                }
                DecreaseEnemiesLeftToSpawn();
                spawnedEnemy.GetComponent<Enemy>().Init(spawnPosition);
                spawnedEnemy.GetComponent<EnemyHealth>().SetHealth(EnemyMaxHealth);
                spawnedEnemy.SetActive(true);

                Animator animator = spawnedEnemy.GetComponent<Animator>();
                if (animator != null)
                {
                    // Trigger the "Spawn" animation.
                    animator.SetTrigger("SpawnTrigger");
                }

                // Disable the EnemyAI script to stop enemy movement initially.
                EnemyAI enemyAI = spawnedEnemy.GetComponent<EnemyAI>();
                if (enemyAI != null)
                {
                    enemyAI.enabled = false;
                }
            }
        }
    }

    void EnableEnemyAI()
    {
        foreach (GameObject enemy in ObjectPoolInstance.pooledObjects_Enemy)
        {
            if (!enemy.activeInHierarchy)
            {
                continue;
            }

            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.enabled = true;
            }
        }
    }


}
