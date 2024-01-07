using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DocksManager : ScenesManager
{
    private GameLevel gameLevel;
    private GameObject player;

    void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player");

        base.Scene = "Docks";
        base.nextScenes = new string[] { "Park","Estates","ShoppingCenter"};
        base.exits = new string[] { Scene + "_North", Scene + "_West", Scene + "_East" };
        RandomizeScenesToExits();
        UpdateStreetSigns();

        UpdateGUI();

    }
}
