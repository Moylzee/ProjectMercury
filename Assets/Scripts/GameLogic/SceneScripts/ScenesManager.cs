
using TMPro;
using UnityEngine;

/* Abstract ScenesManager class, represents all scenes in game*/
public abstract class ScenesManager : MonoBehaviour
{
    public GameObject northTextBox;
    public GameObject eastTextBox;
    public GameObject southTextBox;
    public GameObject westTextBox;
    public string[] nextScenes;
    public string[] exits;
    public string Scene;


    /**
     * Randomize the scenes to the exists
     * 
     */
    public void RandomizeScenesToExits()
    {
        if(nextScenes.Length <= 0 || exits.Length <= 0)
        {
            Debug.LogWarning("Couldn't randomize scenes");
            return;
        }

        for(int i = nextScenes.Length - 1; i > 0; i--)
        {
            int random = Random.Range(0, i + 1);
            string temp = nextScenes[i];
            nextScenes[i] = nextScenes[random];
            nextScenes[random] = temp;
        }

        for (int i = 0; i < exits.Length; i++)
        {
            PlayerPrefs.SetString(exits[i], nextScenes[i]);
        }
    }

    // Method to update the street signs in game
    public void UpdateStreetSigns()
    {
        if(Scene.Length <= 0)
        {
            Debug.LogError("Scene not defined");
            return;
        }
        if(northTextBox != null)
        {
            northTextBox.GetComponentInChildren<TextMeshPro>().text = $"{PlayerPrefs.GetString(Scene + "_North")}";
        }
        if(eastTextBox != null)
        {
            eastTextBox.GetComponent<TextMeshPro>().text = $"{PlayerPrefs.GetString(Scene+"_East")}";
        }
        if(southTextBox != null)
        {
            southTextBox.GetComponent<TextMeshPro>().text = $"{PlayerPrefs.GetString(Scene+"_South")}";
        }
        if(westTextBox != null)
        {
            westTextBox.GetComponent<TextMeshPro>().text = $"{PlayerPrefs.GetString(Scene+"_West")}";
        }
    }

    public void UpdateGUI()
    {
        GameObject playerRef = GameObject.FindWithTag("Player");

        GameObject.FindWithTag("PointText").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Score").ToString();
        GameLevel gameLevel = playerRef.GetComponent<GameLevel>();
        gameLevel.Init();
        gameLevel.UpdateLevelsText();

    }
}