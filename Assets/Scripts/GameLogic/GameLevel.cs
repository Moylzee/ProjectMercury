using System.Collections;
using UnityEngine;
using TMPro;
using System.Text;

public class GameLevel : MonoBehaviour
{

    private TextMeshProUGUI LevelsText;

    // Use this for initialization
    void Awake()
    {
        LevelsText = GameObject.FindWithTag("LevelsText").GetComponent<TextMeshProUGUI>();
    }

    public void Init()
    {
        LevelsText = GameObject.FindWithTag("LevelsText").GetComponent<TextMeshProUGUI>();
    }


    public void UpdateLevelsText()
    {
        int level = PlayerPrefs.GetInt("Level");

        int[] values = new int[] { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
        string[] symbols = new[] { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

        StringBuilder romanValue = new StringBuilder();
        for (int i = 0; i < 13 && level > 0;)
        {
            if (level >= values[i])
            {
                romanValue.Append(symbols[i]);
                level -= values[i];
            }
            else
            {
                i++;
            }
        }

        LevelsText.text = romanValue.ToString();
    }
}
