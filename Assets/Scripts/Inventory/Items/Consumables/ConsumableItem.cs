using UnityEngine;


[System.Serializable]
public class ConsumableItemList
{
    public ConsumableItem[] consumables;
}

[System.Serializable]
public class ConsumableItem : Item
{

    public float HealthChange; // Instant heals or damages
    public float Regeneration; // Change of health over time
    public float RegenerationDurationInSeconds; // Duration of how long regeneration goes on for
    public float TimeToConsumeInSeconds; // Time it takes to consume item in seconds


    public ConsumableItem()
    {

    }

    public void Clone(ConsumableItem itemData)
    {
        setUID();
        SetItemName(itemData.GetItemName());
        SetImageSource(itemData.GetImageSource());
        SetHealthChange(itemData.GetHealthChange());
        SetRegeneration(itemData.GetRegeneration());
        SetRegenerationDurationInSeconds(itemData.GetRegenerationDurationInSeconds());
        SetTimeToConsumeInSeconds(itemData.GetTimeToConsumeInSeconds());
    }

    public void SetHealthChange(float healthChange) { this.HealthChange = healthChange; }
    public void SetRegeneration(float regen) { this.Regeneration = regen; }
    public void SetRegenerationDurationInSeconds(float seconds) { this.RegenerationDurationInSeconds = seconds; }
    public void SetTimeToConsumeInSeconds(float seconds) { this.TimeToConsumeInSeconds = seconds; }


    public float GetHealthChange() { return this.HealthChange; }
    public float GetRegeneration() { return this.Regeneration; }
    public float GetRegenerationDurationInSeconds() { return this.RegenerationDurationInSeconds; }
    public float GetTimeToConsumeInSeconds() { return this.TimeToConsumeInSeconds; }
}
