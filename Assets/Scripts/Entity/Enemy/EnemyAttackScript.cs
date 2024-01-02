using UnityEngine;

/* Script used to handle if the enemy can attack the player
* If the enemy and player box collider is touching, the enemy can attack
* Once the colliders no longer touch, the enemy chases instead of attacks  
*/
public class EnemyAttackScript : MonoBehaviour
{
    private bool canEnemyAttack;
    private PlayerObject player;

    void Start()
    {
        player = FindObjectOfType<PlayerObject>();
    }

    void Update()
    {
        // Check can the enemy attack
        // Calls the attack method every 2 seconds 
        if (canEnemyAttack && !IsInvoking("EnemyAttack"))
        {
            InvokeRepeating("EnemyAttack", 0f, 2f);
        }
        else if (!canEnemyAttack && IsInvoking("EnemyAttack"))
        {
            CancelInvoke("EnemyAttack");
        }
    }

    // Player's box collider touched the enemy.
    // Enemy Can Attack
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canEnemyAttack = true;
        }
    }

    // Player's box collider not touching the enemy.
    // Enemy Can't Attack
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canEnemyAttack = false;
        }
    }

    void EnemyAttack()
    {
        // Access the player's health component and damage them.
        var healthComponent = player.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.DamagePlayer(10);
        }
    }
}