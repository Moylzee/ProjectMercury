using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;


enum DirectionFacing
{
    DOWN = -1,
    UP = 1,
    LEFT = -2,
    RIGHT = 2
}

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
    private SpriteRenderer WeaponInHandRenderer;


    private DirectionFacing isFacing = DirectionFacing.DOWN; 

    [Header("Dash Settings")]
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashDuration = 1f;
    [SerializeField] int dashCost = 10;

    void Start()
    {
        playerStamina = FindObjectOfType<PlayerStamina>();
        playerStamina.SetPlayer(this);

        // Find sprite renderer in hand
        WeaponInHandRenderer = transform.Find("RotatePoint").transform.Find("Equipped").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        
        WeaponRotation();
        
        // Determine the last movement direction
        if (movement != Vector2.zero)
        {
            animator.SetFloat("LastMoveDirectionX", movement.x);
            animator.SetFloat("LastMoveDirectionY", movement.y);

            
            /* Weapon rotation when moving*/
            if (movement.x > 0 && isFacing != DirectionFacing.RIGHT)
            {
                WeaponInHandRenderer.sortingLayerName = "Background";
                WeaponInHandRenderer.flipX = false;
                isFacing = DirectionFacing.RIGHT;
                
            }else if(movement.x < 0 && isFacing != DirectionFacing.LEFT)
            {
                WeaponInHandRenderer.sortingLayerName = "Foreground";
                WeaponInHandRenderer.flipX = true;
                isFacing = DirectionFacing.LEFT;
            }

            if(movement.y > 0 && isFacing != DirectionFacing.UP)
            {
                WeaponInHandRenderer.sortingLayerName = "Foreground";
                WeaponInHandRenderer.flipX = false;
                isFacing = DirectionFacing.UP;
            }
            else if(movement.y < 0 && isFacing != DirectionFacing.DOWN)
            {
                WeaponInHandRenderer.sortingLayerName = "Background";

                WeaponInHandRenderer.flipX = false;
                isFacing = DirectionFacing.DOWN;
            }

        }

        // If RMB is pressed and dash is available (stamina greater than dash cost)
        if (Input.GetMouseButtonDown(1) && canDash && playerStamina.currStamina > dashCost)
        {
            StartCoroutine(Dash());
            playerStamina.IncreaseStamina(-dashCost);
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
        if (playerStamina.currStamina >= 10 && SceneManager.GetActiveScene().name != "StartingRoom")
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

    private void WeaponRotation()
    {

        if(WeaponInHandRenderer == null)
        {
            return;
        }

        Vector3 rotation = Camera.main.ScreenToWorldPoint(Input.mousePosition) - GetComponentInChildren<PlayerShootingBehaviour>().transform.position;
        int offset = 3;
        float rotZ = (Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg) - offset;


        int limitUp = 45 + offset;
        int limitDown = -limitUp;


        if (isFacing == DirectionFacing.DOWN)
        {
            if (WeaponInHandRenderer.flipX == false)
            {
                if (rotZ < -135 && rotZ > -200)
                {

                    rotZ = -135;
                }
                else if (rotZ > 135)
                {
                    rotZ = -135;
                }
                if (rotZ > -90 && rotZ < -85)
                {
                    WeaponInHandRenderer.flipX = true;
                }

                if (rotZ < 135 && rotZ > 91)
                {
                    rotZ *= -1;
                }

                if (rotZ < 90 && rotZ > 80)
                {
                    WeaponInHandRenderer.flipX = true;
                }


                BulletMovement.rotZ = rotZ - 180;
            }
            else
            {
                rotZ -= 180;



                if (rotZ < -280)
                {
                    WeaponInHandRenderer.flipX = false;
                    rotZ += 180;
                }

                if (rotZ > -225 && rotZ < -135)
                {
                    rotZ = -225;
                }
                else if (rotZ > -135 && rotZ < -85)
                {
                    rotZ *= -1;
                }

                if (rotZ > -85 && rotZ < -60)
                {
                    WeaponInHandRenderer.flipX = false;
                    rotZ += 180;
                }
                BulletMovement.rotZ = rotZ;
            }

        }

        if (isFacing == DirectionFacing.UP)
        {
            if (WeaponInHandRenderer.flipX == false)
            {
                if (rotZ < -135 && rotZ > -200)
                {

                    rotZ = -135;
                }
                else if (rotZ > 135)
                {
                    rotZ = -135;
                }
                if (rotZ > -90 && rotZ < -85)
                {
                    WeaponInHandRenderer.flipX = true;
                }

                if (rotZ < 135 && rotZ > 91)
                {
                    rotZ *= -1;
                }

                if (rotZ < 90 && rotZ > 80)
                {
                    WeaponInHandRenderer.flipX = true;
                }
                rotZ *= -1;
                BulletMovement.rotZ = rotZ - 180;
            }
            else
            {
                rotZ -= 180;

                if (rotZ < -280)
                {
                    WeaponInHandRenderer.flipX = false;
                    rotZ += 180;
                }

                if (rotZ > -225 && rotZ < -135)
                {
                    rotZ = -225;
                }
                else if (rotZ > -135 && rotZ < -85)
                {
                    rotZ *= -1;
                }

                if (rotZ > -85 && rotZ < -60)
                {
                    WeaponInHandRenderer.flipX = false;
                    rotZ += 180;
                }
                rotZ *= -1;
                BulletMovement.rotZ = rotZ;
            }


        }


        if (isFacing == DirectionFacing.RIGHT)
        {
            if (rotZ > 135 + offset)
            {
                rotZ -= 180;
                rotZ *= -1;
            }
            else if (rotZ < -135 - offset)
            {
                rotZ += 180;
                rotZ *= -1;
            }


            if (rotZ > limitUp)
            {
                rotZ = limitUp;
            }
            else if (rotZ < limitDown)
            {
                rotZ = limitDown;
            }

            BulletMovement.rotZ = rotZ - 180;
        }
        else if (isFacing == DirectionFacing.LEFT)
        {
            rotZ *= -1;
            if (rotZ > 135 + offset)
            {
                rotZ -= 180;
                rotZ *= -1;
            }
            else if (rotZ < -135 - offset)
            {
                rotZ += 180;
                rotZ *= -1;
            }


            if (rotZ > limitUp)
            {
                rotZ = limitUp;
            }
            else if (rotZ < limitDown)
            {
                rotZ = limitDown;
            }
            BulletMovement.rotZ = rotZ;
        }


        // Display weapon according to rotation
        GetComponentInChildren<PlayerShootingBehaviour>().transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}