using UnityEngine;

public class ConsumableItemObjectBehaviour : ItemObjectBehaviour<ConsumableItem>
{
    public override void Update()
    {
        base.Update();
        InventoryLogic();
    }


    public override void InventoryLogic()
    {
        if (item.IsPickedUp())
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + item.GetOffset();
        }

        if (Input.GetMouseButtonUp(0) && item.IsPickedUp())
        {
            var slot = GameObject.FindWithTag("Slot1").GetComponent<SlotItem>();
            if (slot.IsHoveringOverSlot() && slot.IsSlotEmpty())
            {
                slot.InsertItemInSlot(item);
                Destroy(gameObject);
                return;
            }

            slot = GameObject.FindWithTag("Slot2").GetComponent<SlotItem>();
            if (slot.IsHoveringOverSlot() && slot.IsSlotEmpty())
            {
                slot.InsertItemInSlot(item);
                Destroy(gameObject);
                return;
            }
            slot = GameObject.FindWithTag("Slot3").GetComponent<SlotItem>();
            if (slot.IsHoveringOverSlot() && slot.IsSlotEmpty())
            {
                slot.InsertItemInSlot(item);
                Destroy(gameObject);
                return;
            }

            item.SetPickedUp(false);
            item.SetOnGround(true);
            SetOnGround(true);
            transform.position = (Vector2)GameObject.FindWithTag("Player").transform.position;

        }
    }

    /**
     * Method to apply item effects to the player
     * Affects both health and stamina
     */
    public void UseEffect()
    {
        var Player = GameObject.FindWithTag("Player");
        var PlayerHealth = Player.GetComponent<Health>();
        var PlayerStamina = Player.GetComponent<PlayerStamina>();

        PlayerHealth.HealPlayer((int)item.GetHealthChange());
        PlayerStamina.IncreaseStamina((int)item.GetStaminaChange());
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioScript>().Drink();
        if (Mathf.Abs(item.GetRegeneration()) > 0)
        {
            PlayerHealth.HealPlayerOverTime((int)item.GetRegeneration(), item.GetRegenerationDurationInSeconds());
        }

        if (Mathf.Abs(item.GetStaminaRegeneration()) > 0)
        {
            PlayerStamina.IncreaseStaminaOverTime((int)item.GetStaminaRegeneration(), item.GetStaminaRegenerationDurationInSeconds());
        }

    }
}
