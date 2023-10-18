using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab with the spawn animation.
    public Tilemap tilemap;
    private List<GameObject> spawnedEnemies = new List<GameObject>(); // List to store spawned enemies.

    void Start()
    {
        SpawnEnemies();
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
                spawnedEnemies.Add(spawnedEnemy); // Add the spawned enemy to the list.

                // Assuming you have an Animator component on your enemy prefab.
                Animator animator = spawnedEnemy.GetComponent<Animator>();
                if (animator != null)
                {
                    // Trigger the "Spawn" animation (you should use the actual animation trigger name).
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
        Debug.Log("enabled");
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
