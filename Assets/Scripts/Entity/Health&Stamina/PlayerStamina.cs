using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class used for stamina functionality
*  Keeps track of current and max stamina 
*  Tracks the regen rate of the stamina 
*  Reduces and Increases stamina when needed 
*/
public class PlayerStamina : MonoBehaviour
{
    public int currStamina = 0;
    public int maxStamina = 3;
    private float staminaRegenRate = 30f;
    private float timeSinceLastRegen = 0f;
    private PlayerMovement player;

    void Start()
    {
        currStamina = maxStamina;
    }

    void Update()
    {
        if (currStamina < maxStamina)
        {
            timeSinceLastRegen += Time.deltaTime;

            // Check if enough time has passed to regenerate stamina
            if (timeSinceLastRegen >= staminaRegenRate)
            {
                timeSinceLastRegen = 0f;
                IncreaseStamina(); // Increase stamina
            }
        }
    }

    // Method to reduce stamina 
    public void ReduceStamina()
    {
        currStamina--;
    }

    // Method to increase stamina 
    public void IncreaseStamina()
    {
        currStamina++;
        if (player != null)
        {
            player.canDash = true;
        }
    }

    // Setter 
    public void SetPlayer(PlayerMovement playerMovement) { player = playerMovement; }
}