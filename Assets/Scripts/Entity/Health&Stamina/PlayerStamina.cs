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
    public int maxStamina = 30;
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
                IncreaseStamina(10); // Increase stamina
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


    // Method to increase stamina by n
    public void IncreaseStamina(int stamina)
    {
        this.currStamina += stamina;
        if (currStamina > maxStamina)
        {
            currStamina = maxStamina;
        }

        if(currStamina <= 0)
        {
            currStamina = 0;
        }
    }

    // Method that starts coroutine to increase stamina by n over s seconds
    public void IncreaseStaminaOverTime(int stamina, float seconds)
    {
        StartCoroutine(IncreaseStaminaOverTimeCoroutine(stamina, seconds));
    }

    private IEnumerator IncreaseStaminaOverTimeCoroutine(int stamina, float seconds)
    {
        Debug.LogWarning("This class is running");
        float timePassed = 0f;
        float interval = 0.5f;

        while (timePassed < seconds)
        {
            IncreaseStamina(stamina);
            yield return new WaitForSeconds(interval);

            timePassed += interval;
        }
    }

    // Setter 
    public void SetPlayer(PlayerMovement playerMovement) { player = playerMovement; }
}