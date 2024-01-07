using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EstatesManager : ScenesManager
{
    public GameObject player;

    void Awake()
    {
        base.Scene = "Estates";
        base.nextScenes = new string[] { "Park","Docks","ShoppingCenter"};
        base.exits = new string[] { Scene + "_North", Scene + "_South", Scene + "_East" };

        RandomizeScenesToExits();
        UpdateStreetSigns();

        if (LevelMove.previousScene == "StartingRoom")
        {
            // Set the player's position to the position of the entrance from the inside
            player.transform.position = new Vector3(-138, -41, 0);
        }
        else
        {
            // Set the player's position to the default position
            player.transform.position = new Vector3(-138, -41, 0);
        }

        UpdateGUI();
    }
}