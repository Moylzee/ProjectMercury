using TMPro;
using UnityEngine;

/* PlayerScore class maintains the score text GUI */
public class PlayerScore : MonoBehaviour
{


    private TextMeshProUGUI PointText;

    // Use this for initialization
    void Start()
    {
        PointText = GameObject.FindWithTag("PointText").GetComponent<TextMeshProUGUI>();
    }

    /* Method to update the text GUI */
    public void UpdatePointText()
    {
        PointText.text = PlayerPrefs.GetInt("Score").ToString();
    }
}