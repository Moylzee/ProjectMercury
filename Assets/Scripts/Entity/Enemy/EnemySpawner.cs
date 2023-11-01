using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab with the spawn animation.
    public Tilemap tilemap;
    private List<GameObject> spawnedEnemies = new(); // List to store spawned enemies.
    private int Level = 1;
    public GameObject Player;

    public const int ENEMEY_LIMIT_WAVE = 15;

    private int EnemiesToSpawnInLevel;
    public int EnemiesPerWaveLimit = 15;
    private int EnemiesLeftToSpawnInLevel;

    void Start()
    {
        CalculateEnemiesToSpawn();
        InvokeRepeating("CheckEnemies", 0, 5f);
    }

    void CheckEnemies()
    {
        if(spawnedEnemies.Count <= 0 && EnemiesLeftToSpawnInLevel > 0)
        {
            SpawnEnemies();
        }else if(spawnedEnemies.Count <=  0 && EnemiesLeftToSpawnInLevel == 0)
        {
            // New level
            Level++;
            CalculateEnemiesToSpawn();
        }else if(spawnedEnemies.Count > 0 && EnemiesLeftToSpawnInLevel > 0 &&
               spawnedEnemies.Count < ENEMEY_LIMIT_WAVE)
        {
            SpawnEnemies();
        }

    }


    void CalculateEnemiesToSpawn()
    {
        float Curve = 6;
        float Steepness = 4;

        EnemiesToSpawnInLevel = (int)(Curve * Mathf.Pow(Steepness, Mathf.Log(Level))); // Formula for spawning enemies according to level
        EnemiesLeftToSpawnInLevel = EnemiesToSpawnInLevel;
    }

    void SpawnEnemies()
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int tilePosition in bounds.allPositionsWithin)
        {

           

            TileBase tile = tilemap.GetTile(tilePosition);

            if (tile != null && Player.GetComponent<CircleCollider2D>().OverlapPoint(tilemap.GetCellCenterWorld(tilePosition)))
            {

                EnemiesLeftToSpawnInLevel--;
                if (spawnedEnemies.Count >= EnemiesPerWaveLimit || EnemiesLeftToSpawnInLevel <= 0)
                {
                    return;
                }
                Vector3 spawnPosition = tilemap.GetCellCenterWorld(tilePosition);
                GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                spawnedEnemy.GetComponent<EnemyHealth>().spawnList = spawnedEnemies; // Create reference to spawnedEnemies
                spawnedEnemies.Add(spawnedEnemy); // Add the spawned enemy to the list.

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

                Invoke("EnableEnemyAI", 1.5f);
            }
        }
    }

    void EnableEnemyAI()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.enabled = true;
            }
        }
    }


}
