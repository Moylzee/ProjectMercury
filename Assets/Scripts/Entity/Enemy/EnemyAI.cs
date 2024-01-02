using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform target;
    private float speed = 10f;
    private float checkDistance = 50f;
    public Animator animator;
    LayerMask collisions;
    private float angle = 0;
    private float maxPathFindingTime = 5f;
    private float pathScanningInterval = 1f;
    private float spaceForEnemy = 1f;
    void Start()
    {
        animator = GetComponent<Animator>();
        // Find the player GameObject and get its Transform
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        collisions = LayerMask.GetMask("Collisions");
        if (player != null)
        {
            target = player.transform;
        }
    }
    void FixedUpdate()
    {
        Vector2 playerDirection = (target.position - transform.position).normalized;
        FindPathToTarget(playerDirection);
        transform.position += (Vector3)playerDirection * speed * Time.deltaTime;
        // For Animations 
        // animator.SetFloat("Horizontal", playerDirection.x);
        // animator.SetFloat("Vertical", playerDirection.y);
        // animator.SetFloat("Speed", playerDirection.sqrMagnitude);
    }

    private Vector2 FindPathToTarget(Vector2 toPoint)
    {
        //check if there is obstacle in the way
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.transform.position, Mathf.Infinity, collisions);
        //return the point that was passed if there is nothing in the way
        if (hit.collider != null)
        {
            Debug.DrawLine(transform.position, target.transform.position, Color.red); // Line From Enemy to Player 
            Debug.Log("obstacle");
            //initialise a left and right point at the point of the collision
            Vector2 leftPoint = hit.point;
            Vector2 rightPoint = hit.point;
            //declare two scanners
            RaycastHit2D scannerLeft, scannerRight;
            float elapsedTime = 0f;

            //scan left and right until there is no obstacle or the maximum time is exceeded
while (elapsedTime < maxPathFindingTime)
{
    //move the target of the left and right scanners more left and right
    leftPoint = new Vector2(leftPoint.x - pathScanningInterval, leftPoint.y);
    rightPoint = new Vector2(rightPoint.x + pathScanningInterval, rightPoint.y);

    //do a scan to the left and to the right
    scannerLeft = Physics2D.Raycast(transform.position, leftPoint - (Vector2)transform.position, Vector2.Distance(transform.position, leftPoint), collisions);
    scannerRight = Physics2D.Raycast(transform.position, rightPoint - (Vector2)transform.position, Vector2.Distance(transform.position, rightPoint), collisions);
    //if there is a space, check that there is enough space for the enemy, if so return the point
    if (scannerLeft.collider == null)
    {
        Collider2D obstacleNearHit = Physics2D.OverlapCircle(leftPoint, spaceForEnemy, collisions);
        if (obstacleNearHit == null)
        {
            Debug.DrawLine(transform.position, leftPoint, Color.blue);
            return leftPoint;
        }
    }
    else if (scannerRight.collider == null)
    {
        Collider2D obstacleNearHit = Physics2D.OverlapCircle(rightPoint, spaceForEnemy, collisions);
        if (obstacleNearHit == null)
        {
            Debug.DrawLine(transform.position, rightPoint, Color.blue);
            return rightPoint;
        }
    }

    elapsedTime += Time.deltaTime; // increment elapsedTime
}

// If no path found within maxPathFindingTime, return the original toPoint
return toPoint;

        }else if (hit.collider == null)
        {
            Debug.DrawLine(transform.position, target.transform.position, Color.green);
            return toPoint;
        }
        return transform.position; 
    }
}