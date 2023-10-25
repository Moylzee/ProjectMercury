using System.Collections;
using UnityEngine;

/* Class used to handle the player movement 
* Also handles the dash functionality 
*/
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    bool isDashing;
    public bool canDash = true;
    private PlayerStamina playerStamina;

    [Header("Dash Settings")]
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashDuration = 1f;

    void Start()
    {
        playerStamina = FindObjectOfType<PlayerStamina>();
        playerStamina.SetPlayer(this);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Determine the last movement direction
        if (movement != Vector2.zero)
        {
            animator.SetFloat("LastMoveDirectionX", movement.x);
            animator.SetFloat("LastMoveDirectionY", movement.y);
        }

        // If RMB is pressed and dash is available
        if (Input.GetMouseButtonDown(1) && canDash && playerStamina.currStamina > 0)
        {
            StartCoroutine(Dash());
            playerStamina.IncreaseStamina(-10);
        }
    }

    void FixedUpdate()
    {
        // Don't Run this function if in the middle of a dash  
        if (isDashing)
        {
            return;
        }
        // If there is stamina available, the player can dash 
        if (playerStamina.currStamina >= 10)
        {
            canDash = true;
        }
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement.normalized);
    }
    private IEnumerator Dash()
    {
        // Can't dash again while already Dashing
        canDash = false;
        isDashing = true;
        // Dash Functionality 
        rb.velocity = new Vector2(movement.x * dashSpeed, movement.y * dashSpeed);
        // Dash for set amount of time 
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        
    }
}