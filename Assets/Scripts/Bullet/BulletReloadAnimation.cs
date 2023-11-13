using System.Collections;
using UnityEngine;

public class BulletReloadAnimation : MonoBehaviour
{

    public float arcHeight = 7.25f; // Adjust the arc height as needed.
    public float rotationSpeed = 30.0f; // Adjust the rotation speed as needed.
    public float speed = 4.5f;
    private float time = 0.0f;
    private Vector3 initialPosition;
    private bool isMoving = true;
    private int dirX = 1;
    private int dirY = 1;
    void Start()
    {
        // Store the initial position of the object.
        initialPosition = transform.position;
        dirX = Random.Range(-1, 1);
        dirY = Random.Range(-1, 1);

    }

    void Update()
    {
        if (isMoving)
        {
            // Update time in each frame.
            time += Time.deltaTime * speed;
            
            // Calculate the position in an arc relative to the initial position.
            float xPos = initialPosition.x + dirX * (Mathf.Sin(time) * arcHeight);
            float yPos = initialPosition.y + (dirY * time * arcHeight);

            // Set the object's position.
            transform.position = new Vector3(xPos, yPos, 0);

            // Rotate the object.
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

            // Check if we've completed half of the arc and stop moving.
            if (time >= Mathf.PI / 1.3f) // Stops at pi (half of a full circle)
            {
                isMoving = false;
            }
        }
    }


}