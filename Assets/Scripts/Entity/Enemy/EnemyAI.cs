using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Animator animator;
    // Target to chase 
    public GameObject target;
    // enemy that will use pathfinding
    public Transform enemyGFX;
    public float speed = 8f;
    public float nextWaypoint = 3f;
    Path path;
    int currWaypoint = 0;
    bool reachedEnd = false;
    Seeker seeker;
    Rigidbody2D rb;
    private bool enabled;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Find the player with tag "Player"
        target = GameObject.FindWithTag("Player");

        if (target == null)
        {
            Debug.LogError("Player not found with tag 'Player'. Make sure to tag your player GameObject.");
        }

        InvokeRepeating("UpdatePath", 0f, .5f);
        enabled = false;
    }

    public void EnableAI()
    {
        enabled = true;
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currWaypoint = 0;

            // Enable the AI script when the path is complete.
            EnableAI();
        }
    }
    void Update()
    {
        if (path == null)
        {
            return;
        }

        if (currWaypoint >= path.vectorPath.Count)
        {
            reachedEnd = true;
            return;
        }
        else
        {
            reachedEnd = false;
        }

        Vector2 targetPosition = path.vectorPath[currWaypoint];
        Vector2 direction = (targetPosition - rb.position).normalized;

        // Calculate a point ahead of the enemy on the path.
        float lookAheadDistance = 4f;
        Vector2 lookAheadPoint = rb.position + direction * lookAheadDistance;

        // Offset the lookAheadPoint to encourage smoother corner navigation.
        float pathOffset = 1.2f;
        Vector2 offsetPoint = Vector2.Lerp(rb.position, lookAheadPoint, pathOffset);

        // Calculate the force to steer towards the offsetPoint.
        Vector2 force = (offsetPoint - rb.position).normalized * speed * Time.deltaTime;

        rb.AddForce(force);

        // Check if the enemy is close enough to the waypoint to move to the next one.
        float dist = Vector2.Distance(rb.position, targetPosition);

        if (dist < nextWaypoint)
        {
            currWaypoint++;
        }

        // For Animations 
        animator.SetFloat("Horizontal", force.x);
        animator.SetFloat("Vertical", force.y);
        animator.SetFloat("Speed", force.sqrMagnitude);
    }
}