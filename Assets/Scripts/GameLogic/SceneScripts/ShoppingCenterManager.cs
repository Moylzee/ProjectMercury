using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShoppingCenterManager : ScenesManager
{
    public GameObject player;
    void Awake()
    {
        base.Scene = "ShoppingCenter";
        base.nextScenes = new string[] { "Estates","Docks","Park"};
        base.exits = new string[] { Scene + "_West", Scene + "_South", Scene + "_East" };

        RandomizeScenesToExits();
        UpdateStreetSigns();

        if (LevelMove.previousScene == "ShoppingCenterInside")
        {
            // Set the player's position to the position of the entrance from the inside
            player.transform.position = new Vector3(160, 80, 0);
        }
        else
        {
            // Set the player's position to the default position
            player.transform.position = new Vector3(160, 0, 0);
        }

        UpdateGUI();
    }
}