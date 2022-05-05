using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestorePowerup : BasePowerup
{
    private float timeToDespawn = 10f;
    private int healAmount = 1;

    private void OnEnable()
    {
        Invoke(nameof(DespawnPowerup), timeToDespawn);
    }

    public override void TriggerEffect(Collider2D collision)
    {
        base.TriggerEffect(collision);

        var healthSystem = collision.GetComponent<HealthSystem>();

        if(healthSystem == null)
        {
            return;
        }

        healthSystem.Heal(healAmount);

        DespawnPowerup();
        GameEvents.PowerupCollected();
    }

    public void ChangeHealAmount(int value)
    {
        healAmount = value;
    }
}
