using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCamera;
    private Rigidbody2D rb;
    public float force;

    public static float rotZ;



    Vector2 direction;
    public void Init(Vector3 pos)
    {

        transform.position = pos;
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();


        transform.rotation = Quaternion.Euler(0, 0, rotZ);



        // Destory bullet after n seconds
        Invoke("DeactivateGameObject", 4f);
    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {

        if (gameObject.activeInHierarchy)
        {
            transform.Translate(-force * Time.deltaTime, 0, 0);
        }
    }
}