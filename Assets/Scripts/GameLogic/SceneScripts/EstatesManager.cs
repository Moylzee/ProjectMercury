using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EstatesManager : MonoBehaviour
{
    public TextMeshProUGUI northTextBox;
    public TextMeshProUGUI eastTextBox;
    public TextMeshProUGUI southTextBox;

    public Transform northTextBoxObject;
    public Transform eastTextBoxObject;
    public Transform southTextBoxObject;

    public string[] nextScenes = { "Forest", "Docks", "ShoppingCentre" };
    public string[] estateExits = { "Estates_North", "Estates_South", "Estates_East" };

    void Start()
    {
        AssignScenesToExits();
        UpdateStreetSign();
        PositionStreetSigns();
    }


    void AssignScenesToExits()
    {
        for (int i = nextScenes.Length - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i + 1);
            string temp = nextScenes[i];
            nextScenes[i] = nextScenes[rnd];
            nextScenes[rnd] = temp;
        }

        for (int i = 0; i < estateExits.Length; i++)
        {
            PlayerPrefs.SetString(estateExits[i], nextScenes[i]);
        }
    }

    private void UpdateStreetSign()
    {
        northTextBox.text = $"{PlayerPrefs.GetString("Estates_North")}";
        eastTextBox.text = $"{PlayerPrefs.GetString("Estates_East")}";
        southTextBox.text = $"{PlayerPrefs.GetString("Estates_South")}";
    }

    void PositionStreetSigns()
    {
        SetUITextPosition(northTextBox, northTextBoxObject);
        SetUITextPosition(eastTextBox, eastTextBoxObject);
        SetUITextPosition(southTextBox, southTextBoxObject);
    }

    void SetUITextPosition(TextMeshProUGUI uiText, Transform textBox)
    {
        if (uiText != null && textBox != null)
        {
            Vector3 worldPos = textBox.transform.position;

            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

            uiText.rectTransform.position = screenPos;
        }
    }
}
