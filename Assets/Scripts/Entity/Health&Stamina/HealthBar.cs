using UnityEngine;
using UnityEngine.UI;

/* This script handles the UI for the health Bar
* Tracks the player's health and changes the health bar accordingly
*/
public class HealthBar : MonoBehaviour
{
    public Health playerHealth;
    public Image fillImage;
    void Start()
    {
        // Health Bar is red 
        fillImage.color = Color.red;
    }

    void Update()
    {
        float fillValue = (float)playerHealth.currHealth / playerHealth.maxHealth;
        // Set the fill amount of the image.
        fillImage.fillAmount = fillValue;
        // Toggle the image component based on health.
        fillImage.enabled = playerHealth.currHealth > 0;
    }
}
