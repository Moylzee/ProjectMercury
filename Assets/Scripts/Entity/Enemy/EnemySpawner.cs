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

    void Start()
    {
        SpawnEnemies();
        InvokeRepeating("CheckEnemies", 0, 5f);
    }

    void CheckEnemies()
    {
        if(spawnedEnemies.Count <= 0)
        {
            Level++;
            for(int i = 0; i < Level; i++)
            {
                SpawnEnemies();
            }
        }

        Debug.LogWarning("There are "+ spawnedEnemies.Count+ " left on the map");
    }

    void SpawnEnemies()
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int tilePosition in bounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(tilePosition);

            if (tile != null)
            {
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
