using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/*
/ Script for the Settings menu In the game 
/ Handles Volume, fullscreen, resolution and quality
*/

public class OptionsMenu : MonoBehaviour
{
    // Variable for the audio mixer 
    public AudioMixer audioMixer;
    // Variable for the resolution dropdown
    public TMPro.TMP_Dropdown resolutionDropdown;
    // Array to store resolution options 
    Resolution[] resolutions;
    int currRes = 0;

    void Start()
    {
        // Get the resolution options and add them to the dropdown
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> reso = new List<string>();
        // Loop through the resolutions and add them to the list
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            reso.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currRes = i;
            }
        }
        resolutionDropdown.AddOptions(reso);
        resolutionDropdown.value = currRes;
        resolutionDropdown.RefreshShownValue();
    }
    // Method to set the volume of the game 
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    // Method to set the graphics quality of the game 
    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }
    // Method to set to fullscreen 
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    // Method to set the resolution of the game 
    public void SetRes(int resIndex)
    {
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}