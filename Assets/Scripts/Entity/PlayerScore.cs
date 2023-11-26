using System.Collections;
using TMPro;
using UnityEngine;


public class PlayerScore : MonoBehaviour
{


    private TextMeshProUGUI PointText;

    // Use this for initialization
    void Start()
    {
        PointText = GameObject.FindWithTag("PointText").GetComponent<TextMeshProUGUI>();
    }


    public void UpdatePointText()
    {
        PointText.text = PlayerPrefs.GetInt("Score").ToString();
    }
}