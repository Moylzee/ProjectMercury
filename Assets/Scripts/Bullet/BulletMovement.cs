using UnityEngine;

/*
 * BulletMovement Script
 * Contains initialisation code for the bullet in order to move in a given direction
 * 
 */
public class BulletMovement : MonoBehaviour
{
    // Constant to determine the longevity of the bullet lifespan
    private const float LIMIT_LIFETIME = 4f; // in seconds 

    // Bullet attributes
    public float force;
    
    /* rotZ is static as is affected by rotation of player globally */
    public static float rotZ;


    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    /* Init method, run when activating from object pool */
    public void Init(Vector3 pos)
    {
        transform.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, rotZ));

        // Destory bullet after n seconds
        Invoke("DeactivateGameObject", LIMIT_LIFETIME);
    }

    /* Method to deactive bullet gameobject */
    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    // Apply force every update
    void Update()
    {

        if (gameObject.activeInHierarchy)
        {
            transform.Translate(-force * Time.deltaTime, 0, 0);
        }
    }
}