using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCamera;
    private Rigidbody2D rb;
    public float force;

    Vector2 direction;

    void Start()
    {

    }


    public void Init(Vector3 pos)
    {

        transform.position = pos;
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();


        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        direction = (Vector2)mousePos - (Vector2)transform.position;
        Vector3 rotation = transform.position - mousePos;

        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        rb.velocity = direction.normalized * force;
        transform.rotation = Quaternion.Euler(0, 0, rot);

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