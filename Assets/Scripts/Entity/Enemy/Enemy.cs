using UnityEngine;

/*
 * Enemy Script
 * Main script for enemy
 */
public class Enemy : MonoBehaviour
{
    // Reference to ObjectPool
    private ObjectPool poolInstance;


    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    /* Initialisation of EnemyObject when activating pooled objects */
    public void Init(Vector3 pos)
    {
       
        transform.position = pos;
        poolInstance = ObjectPool.SharedInstance;
    }


    public void DecrementCurrentActiveEnemies()
    {
        poolInstance.currentActiveEnemies--;
    }

}
