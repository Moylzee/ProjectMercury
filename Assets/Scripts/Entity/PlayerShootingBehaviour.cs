using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingBehaviour : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    private float bulletLifeSpan = 4f;
    public Transform bulletTransform;
    private bool canFire = true;
    private float timer;
    public float fireRate;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > fireRate)
            {
                canFire = true;
                timer = 0;
            }
        }
        // if firing is possible and the LMB is clicked
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            canFire = false;
            GameObject newBullet = Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            // Call the destroy bullet method after the lifespan
            StartCoroutine(DestroyBullet(newBullet, bulletLifeSpan));
        
        }
    }

    // Method to destroy the bullet after its lifespan
    private IEnumerator DestroyBullet(GameObject bulletObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bulletObject);
    }
}
