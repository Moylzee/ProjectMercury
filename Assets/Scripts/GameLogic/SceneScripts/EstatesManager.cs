
public class EstatesManager : ScenesManager
{

    private void Start()
    {

        base.Scene = "Estates";
        base.nextScenes = new string[] { "Forest","Docks","ShoppingCentre"};
        base.exits = new string[] { Scene+"_North", Scene + "_South", Scene + "_East" };


        RandomizeScenesToExists();
        UpdateStreetSigns();
    }


}