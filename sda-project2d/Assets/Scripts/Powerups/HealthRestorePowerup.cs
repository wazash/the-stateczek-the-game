using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestorePowerup : BasePowerup
{
    private float timeToDespawn = 10f;
    private int healAmount = 1;

    private void Start()
    {
        GameEvents.OnGameStarted += GameEvents_OnGameStarted;
    }

    private void OnDestroy()
    {
        GameEvents.OnGameStarted -= GameEvents_OnGameStarted;
    }

    private void GameEvents_OnGameStarted()
    {
        ResetHealAmount();
    }

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
    private void ResetHealAmount()
    {
        healAmount = 1;
    }
}
