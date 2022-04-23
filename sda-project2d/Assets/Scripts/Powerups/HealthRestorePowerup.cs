using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestorePowerup : BasePowerup
{
    public override void TriggerEffect(Collider2D collision)
    {
        base.TriggerEffect(collision);

        var healthSystem = collision.GetComponent<HealthSystem>();

        if(healthSystem == null)
        {
            return;
        }

        healthSystem.Heal(3);

        DespawnPowerup();
    }
}
