using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 Enemy Spawner class, responsible for spawning enemies in waves for each round
 */

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab with the spawn animation.
    public Tilemap tilemap;
    public GameObject Player;

    // Wave Attributes
    private int EnemyMaxHealth = 30;
    private int EnemySpeed = 15;
    public int ENEMEY_LIMIT_WAVE;
    private static int EnemiesLeftToSpawnInLevel = 0;
    private float EnemeySpawnDelay = 2f;
    private GameLevel gameLevel;

    private const float CHECK_ENEMIES_DELAY = 5f;
    private const float ENEMIES_AI_DELAY = 1.5f;

    private ObjectPool ObjectPoolInstance;

    void Start()
    {
        ObjectPoolInstance = ObjectPool.SharedInstance;

        gameLevel = GameObject.FindWithTag("Player").GetComponent<GameLevel>();

        ENEMEY_LIMIT_WAVE = ObjectPoolInstance.ENEMY_LIMIT;

        CalculateEnemiesToSpawnInLevel();
        InvokeRepeating("CheckEnemies", EnemeySpawnDelay, CHECK_ENEMIES_DELAY);
    }

    /* Method to calculate how many enemies to spawn per round */
    void CalculateEnemiesToSpawnInLevel()
    {
        // When Level is below 20
        if(PlayerPrefs.GetInt("Level") < 20)
        {
            EnemiesLeftToSpawnInLevel = 5 + (int)Mathf.Pow(6, Mathf.Log10(PlayerPrefs.GetInt("Level")));
            return;
        }else if(PlayerPrefs.GetInt("Level") >= 80)
        {
            EnemiesLeftToSpawnInLevel = 5 * ObjectPoolInstance.amountToPool_Enemy;
            return;
        }

        EnemiesLeftToSpawnInLevel = (int)(Mathf.Pow(4, Mathf.Log10(PlayerPrefs.GetInt("Level") * 10)) + 11);
    }

    /* Method to calculate new health for enemy after a new round */
    void CalculateEnemyHealth()
    {
        this.EnemyMaxHealth = (int)(30 + Mathf.Pow(4, Mathf.Log10(PlayerPrefs.GetInt("Level"))));
    }

    /* Method to calculate new speed for enemy after a new round */
    void CalculateEnemySpeed()
    {
        this.EnemySpeed = 15 + (int)Mathf.Pow(2.5f, Mathf.Log10(PlayerPrefs.GetInt("Level")));
        if(EnemySpeed > 20)
        {
            this.EnemySpeed = 20;
        }
    }

    /* Method to check whether to spawn enemies */
    void CheckEnemies()
    {
        // There are no active enemies and there is still more enemies to spawn
        if(ObjectPoolInstance.currentActiveEnemies <= 0 && EnemiesLeftToSpawnInLevel > 0)
        {
            SpawnEnemies();
        }
        // There are no active enemies and there is no more enemies to spawn
        else if(ObjectPoolInstance.currentActiveEnemies <= 0 && EnemiesLeftToSpawnInLevel <= 0)
        {
            // Increase level and update GUI
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            PlayerPrefs.Save();
            gameLevel.UpdateLevelsText();


            // Determine amount of enemies and their stats for new level
            CalculateEnemiesToSpawnInLevel();
            CalculateEnemyHealth();
            CalculateEnemySpeed();

            // Spawn Ememies
            SpawnEnemies();
        }
        // Active enemies haven't reach wave limit and more enemies are yet to spawn
        else if(ObjectPoolInstance.currentActiveEnemies < ENEMEY_LIMIT_WAVE && EnemiesLeftToSpawnInLevel > 0)
        {
            SpawnEnemies();
        }

        Invoke("EnableEnemyAI", ENEMIES_AI_DELAY);
    }

    /* Spawn Enemies Method */
    void SpawnEnemies()
    {
        BoundsInt bounds = tilemap.cellBounds;
        // Loops through all possible spawning points
        foreach(Vector3Int tilePosition in bounds.allPositionsWithin)
        {
            // No more enemies left to spawn right now
            if(ObjectPoolInstance.currentActiveEnemies >= ENEMEY_LIMIT_WAVE || EnemiesLeftToSpawnInLevel <= 0)
            {
                return;
            }

            TileBase tile = tilemap.GetTile(tilePosition);
            // Check that tile is not null and within spawnable area
            if (tile != null && Player.GetComponent<CircleCollider2D>().OverlapPoint(tilemap.GetCellCenterWorld(tilePosition)))
            {



                Vector3 spawnPosition = tilemap.GetCellCenterWorld(tilePosition);
                // retrieve enemy object from pool
                GameObject spawnedEnemy = ObjectPoolInstance.GetPooledObjectEnemy();
                if (spawnedEnemy == null)
                {
                    return;
                }

                EnemiesLeftToSpawnInLevel--;
                // Initialize the enemy
                spawnedEnemy.GetComponent<Enemy>().Init(spawnPosition);
                spawnedEnemy.GetComponent<EnemyHealth>().SetHealth(EnemyMaxHealth);
                spawnedEnemy.GetComponent<EnemyAI>().SetSpeed(EnemySpeed);
                spawnedEnemy.SetActive(true);
                Animator animator = spawnedEnemy.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetTrigger("SpawnTrigger");
                }

                EnemyAI enemyAI = spawnedEnemy.GetComponent<EnemyAI>();
                if (enemyAI != null)
                {
                    enemyAI.enabled = false;
                }
            }
        }
    }

    /* Enable Enemy Movement */
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
