using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    public int sceneIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Switching Scene to " + sceneIndex);

            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }
}
