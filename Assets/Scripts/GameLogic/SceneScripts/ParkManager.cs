using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkManager : ScenesManager
{
    public GameObject player;
    void Start()
    {
        base.Scene = "Park";
        base.nextScenes = new string[] { "Estates","Docks","ShoppingCenter"};
        base.exits = new string[] { Scene + "_West", Scene + "_South", Scene + "_East" };

        RandomizeScenesToExits();
        UpdateStreetSigns();

        player.transform.position = new Vector3(5, 135, 0);
    }
}