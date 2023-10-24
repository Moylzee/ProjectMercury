using UnityEngine;


[System.Serializable]
public class ConsumableItemList
{
    public ConsumableItem[] consumables;
}

[System.Serializable]
public class ConsumableItem : Item
{

    public float HealthChange = 0; // Instant heals or damages
    public float Regeneration = 0; // Change of health over time
    public float RegenerationDurationInSeconds = 0; // Duration of how long regeneration goes on for
    public float TimeToConsumeInSeconds = 0; // Time it takes to consume item in seconds

    public float StaminaChange = 0;
    public float StaminaRegeneration = 0;
    public float StaminaRegenerationDurationInSeconds = 0;


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
        SetStaminaChange(itemData.GetStaminaChange());
        SetStaminaRegeneration(itemData.GetStaminaRegeneration());
        SetStaminaRegenerationDurationInSeconds(itemData.GetStaminaRegenerationDurationInSeconds());
    }

    public void SetHealthChange(float healthChange) { this.HealthChange = healthChange; }
    public void SetRegeneration(float regen) { this.Regeneration = regen; }
    public void SetRegenerationDurationInSeconds(float seconds) { this.RegenerationDurationInSeconds = seconds; }
    public void SetTimeToConsumeInSeconds(float seconds) { this.TimeToConsumeInSeconds = seconds; }
    public void SetStaminaChange(float staminaChange) { this.StaminaChange = staminaChange; }

    public void SetStaminaRegeneration(float regen) { this.StaminaRegeneration = regen; }
    public void SetStaminaRegenerationDurationInSeconds(float seconds) { this.StaminaRegenerationDurationInSeconds = seconds; }
    public float GetHealthChange() { return this.HealthChange; }
    public float GetRegeneration() { return this.Regeneration; }
    public float GetRegenerationDurationInSeconds() { return this.RegenerationDurationInSeconds; }
    public float GetTimeToConsumeInSeconds() { return this.TimeToConsumeInSeconds; }

    public float GetStaminaChange() { return this.StaminaChange; }
    public float GetStaminaRegeneration() { return this.StaminaRegeneration; }
    public float GetStaminaRegenerationDurationInSeconds() { return this.StaminaRegenerationDurationInSeconds; }
}
