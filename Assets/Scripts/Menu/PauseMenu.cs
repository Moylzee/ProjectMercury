using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    private static bool isGameOver = false;
    public GameObject GameOverPanel;
    public static bool InventoryOpen = false;
    public GameObject pauseUI;
    public GameObject inventoryUI;
    public GameObject weaponsHotbar;
    public GameObject itemsHotbar;

    void Update()
    {
        // Pause Menu Triggered
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        // Inventory Menu Triggered
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (InventoryOpen)
            {
                CloseInv();
            }
            else
            {
                OpenInv();
            }
        }

        // Game is over, show the pause panel and freeze game elements.
        if (isGameOver)
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
        }
        else
        {
            GameOverPanel.SetActive(false);
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    void OpenInv() 
    {
        inventoryUI.SetActive(true);
        weaponsHotbar.SetActive(false);
        itemsHotbar.SetActive(false);
        InventoryOpen = true;
    }

    void CloseInv()
    {
        inventoryUI.SetActive(false);
        weaponsHotbar.SetActive(true);
        itemsHotbar.SetActive(true);
        InventoryOpen = false;
    }

    public void GameOver()
    {
        isGameOver = true;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}