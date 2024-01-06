using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform target;
    private LayerMask collisions;

    private readonly float maxDistance = 250f;
    private float recheckInterval = 5f; // Time interval to recheck path
    private float recheckTimer = 0f;

    private Vector2 directionToMove = Vector2.zero;
    private int count = 0;
    private readonly int maxIterations = 400;
    private float angleOffsetIntravel = 20f;
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        collisions = LayerMask.GetMask("Collisions");
    }


    private void FixedUpdate()
    {
        if(directionToMove != Vector2.zero)
        {
            Debug.DrawLine(transform.position, new Vector2(directionToMove.x, directionToMove.y), Color.green);
            transform.position += 15f * Time.fixedDeltaTime * (Vector3)directionToMove;
        }
        else
        {
            // If there is no direction to move find one
            directionToMove = FindPath();
        }

        // If time elapsed recheck path
        recheckTimer += Time.fixedDeltaTime;

        if (recheckTimer > recheckInterval)
        {
            directionToMove = FindPath();
            recheckTimer = 0f;
        }


    }

    /* Method to find path to target */
    private Vector2 FindPath()
    {
        Vector2 directionToTarget = ((Vector2)target.position - (Vector2)transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, Vector2.Distance(target.position, transform.position), collisions);

        if(hit.collider == null)
        {
            // Direct path exists
            recheckInterval = 0.1f;
            return directionToTarget;
        }
        else
        {
            // No direct path exists
            recheckInterval = 2f;
            Vector2 longestDirection = ShootRays(transform.position, directionToTarget, 0);
            return longestDirection;
        }
    }

    /* Shoots rays recursively until a suitable ray is found*/
    private Vector2 ShootRays(Vector2 fromPosition, Vector2 direction, float angleOffset)
    {
        if(count > maxIterations)
        {
            return Vector2.zero; // may need to change
        }

        float angle1 = Mathf.Atan2(direction.y, direction.x) + Mathf.Deg2Rad * angleOffset;
        float angle2 = angle1 - Mathf.Deg2Rad * angleOffset * 2f;

        Vector2 adjustedDirection1 = new Vector2(Mathf.Cos(angle1), Mathf.Sin(angle1));
        Vector2 adjustedDirection2 = new Vector2(Mathf.Cos(angle2), Mathf.Sin(angle2));

        RaycastHit2D hitInfo1 = Physics2D.Raycast(fromPosition, adjustedDirection1, maxDistance, collisions);
        RaycastHit2D hitInfo2 = Physics2D.Raycast(fromPosition, adjustedDirection2, maxDistance, collisions);

        Debug.DrawLine(fromPosition, hitInfo1.point, Color.yellow);
        Debug.DrawLine(fromPosition, hitInfo2.point, Color.yellow);

        // If the two rays are similar in distance
        if(hitInfo1.distance + 5f > hitInfo2.distance && hitInfo2.distance + 5f > hitInfo1.distance)
        {
            count++;
            return ShootRays(fromPosition, direction, angleOffset + angleOffsetIntravel);
        }
        else
        {
            return hitInfo1.distance > hitInfo2.distance ? adjustedDirection1 : adjustedDirection2;
        }

    }

}