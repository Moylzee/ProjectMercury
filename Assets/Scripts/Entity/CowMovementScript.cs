using UnityEngine;

/*
/ Script to Handle the movement of the cows in the park 
/ The cows are animated game objects 'NPCs' used to add aesthetic to the park scene 
*/

public class CowMovementScript : MonoBehaviour
{
    public Rigidbody2D rb;
    private float totalDistance = 0f;
    private int direction = 1;
    private float pauseTimer = 0f;
    public Animator animator;
    void Start()
    {
        // Determine what direction the cow should move based on the direction it is facing 
        direction = transform.rotation.y == 0 ? 1 : -1;
    }

    void FixedUpdate() 
    {
        // If the cow is idle, check when pause timer reaches 0 to let it move again
        if (pauseTimer > 0)
        {
            // Countdown timer 
            pauseTimer -= Time.fixedDeltaTime;
            // Set the animation to idle based on 'speed' parameter
            animator.SetFloat("Speed", 0);
            return;
        }

        float moveDistance = 5f * Time.fixedDeltaTime;
        totalDistance += moveDistance;
        // Check if cow moves the max distance 
        if (totalDistance >= 25f)
        {
            // Reset cow's movement and change its movement direction
            direction *= -1; 
            totalDistance = 0f; 
            // Give cow random pause time between 1 and 10
            pauseTimer = Random.Range(1f, 10f);
            // Rotate the game object so it is facing the correct direction
            transform.Rotate(0, 180, 0);
        }
        else
        {
            rb.MovePosition(rb.position + moveDistance * direction * new Vector2(1,0).normalized);
            // Set the animation to walking based on 'speed' parameter
            animator.SetFloat("Speed", 1);
        }
    }
}