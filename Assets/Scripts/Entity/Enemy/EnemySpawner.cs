using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab with the spawn animation.
    public Tilemap tilemap;
    public GameObject Player;

    // Wave Attributes
    private int EnemyMaxHealth = 30;
    public int ENEMEY_LIMIT_WAVE;
    private int EnemiesLeftToSpawnInLevel = 0;
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

        CalculateEnemiesToSpawn();
        InvokeRepeating("CheckEnemies", EnemeySpawnDelay, CHECK_ENEMIES_DELAY);
    }

    void CheckEnemies()
    {
        // If theres no enemies left but theres more to spawn, spawn more
        if (ObjectPoolInstance.currentActiveEnemies <= 0 && GetEnemiesLeftToSpawn() > 0)
        {
            SpawnEnemies();

        }
        // If there's no enemies left, and no more to spawn, new round
        else if (ObjectPoolInstance.currentActiveEnemies <= 0 && GetEnemiesLeftToSpawn() == 0)
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            gameLevel.UpdateLevelsText();
            Debug.Log("New Round! Round: " + PlayerPrefs.GetInt("Level"));

            CalculateEnemiesToSpawn();
            CalculateEnemiesHealth();
            SpawnEnemies();
        }
        // If theres more zombies to spawn and the max zombies hasn't been reached, spawn more!
        else if (ObjectPoolInstance.currentActiveEnemies < ENEMEY_LIMIT_WAVE && GetEnemiesLeftToSpawn() > 0)
        {
            SpawnEnemies();
        }

        Invoke("EnableEnemyAI", ENEMIES_AI_DELAY);

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
        this.EnemyMaxHealth = (int)(Mathf.Pow(2.4f, Mathf.Log(PlayerPrefs.GetInt("Level"))) + 30);
    }

    /* Function to calculate how many enemies per round */
    void CalculateEnemiesToSpawn()
    {

        if (PlayerPrefs.GetInt("Level") < 20)
        {
            this.EnemiesLeftToSpawnInLevel = 5 + (int)Mathf.Pow(6, Mathf.Log10(PlayerPrefs.GetInt("Level")));
            return;
        }
        else if (PlayerPrefs.GetInt("Level") >= 80)
        {
            this.EnemiesLeftToSpawnInLevel = 5 * ObjectPoolInstance.amountToPool_Enemy; // 3 waves
            return;
        }


        this.EnemiesLeftToSpawnInLevel = (int)(Mathf.Pow(4, Mathf.Log10(PlayerPrefs.GetInt("Level") * 10)) + 11);
        
    }

    /* Function to spawn enemies*/
    void SpawnEnemies()
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int tilePosition in bounds.allPositionsWithin)
        {

            // If object pool reached return
            if (ObjectPoolInstance.currentActiveEnemies >= ENEMEY_LIMIT_WAVE || GetEnemiesLeftToSpawn() <= 0)
            {
                return;
            }

            TileBase tile = tilemap.GetTile(tilePosition);

            if (tile != null && Player.GetComponent<CircleCollider2D>().OverlapPoint(tilemap.GetCellCenterWorld(tilePosition)))
            {
                Vector3 spawnPosition = tilemap.GetCellCenterWorld(tilePosition);

                GameObject spawnedEnemy = ObjectPoolInstance.GetPooledObjectEnemy();

                if (spawnedEnemy == null)
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
        EnableEnemyAI();
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
                Debug.Log("Enemy AI enabled");
                enemyAI.enabled = true;
            }else {
                Debug.Log("Enemy AI is null");
            }
        }
    }


}
