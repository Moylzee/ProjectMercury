using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocksManager : ScenesManager
{
    void Awake()
    {
        base.Scene = "Docks";
        base.nextScenes = new string[] { "Park","Estates","ShoppingCenter"};
        base.exits = new string[] { Scene + "_North", Scene + "_West", Scene + "_East" };
        RandomizeScenesToExits();
        UpdateStreetSigns();
    }
}
