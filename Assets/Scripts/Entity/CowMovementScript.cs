using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowMovementScript : MonoBehaviour
{
public Rigidbody2D rb;
private float totalDistance = 0f;
private int direction = 1;
private float pauseTimer = 0f;
public Animator animator;
void Start()
{
    direction = transform.rotation.y == 0 ? 1 : -1;
}

void FixedUpdate() 
{

    if (pauseTimer > 0)
    {
        pauseTimer -= Time.fixedDeltaTime;
        animator.SetFloat("Speed", 0);
        return;
    }

    float moveDistance = 5f * Time.fixedDeltaTime;
    totalDistance += moveDistance;

    if (totalDistance >= 25f)
    {
        direction *= -1; 
        totalDistance = 0f; 
        pauseTimer = Random.Range(1f, 10f);
        transform.Rotate(0, 180, 0);
    }
    else
    {
        rb.MovePosition(rb.position + moveDistance * direction * new Vector2(1,0).normalized);
        animator.SetFloat("Speed", 1);
    }
}
}