using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class BasePowerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEffect(collision);
    }

    public virtual void TriggerEffect(Collider2D collision)
    {

    }

    public void DespawnPowerup()
    {
        gameObject.SetActive(false);
    }
}
