using UnityEngine;
using UnityEngine.UI;
/* Class used to update the Stamina Bar UI
* Uses the values from the stamina class to determine how much stamina to display
*/
public class StaminaBar : MonoBehaviour
{

    public PlayerStamina playerStamina;
    public Image fillImage;
    void Start()
    {
        fillImage.color = Color.blue;
    }

    void Update()
    {
        float fillValue = (float)playerStamina.currStamina / playerStamina.maxStamina;
        fillImage.fillAmount = fillValue;
        fillImage.enabled = playerStamina.currStamina > 0;
    }
}