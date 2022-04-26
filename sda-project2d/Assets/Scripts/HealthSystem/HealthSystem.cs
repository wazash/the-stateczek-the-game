using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event Action OnHealthDepleted;
    public event Action<int> OnHealthChanged;

    [SerializeField] private int hpAmountTotal;
    private int currentHP;

    public int CurrentHP { get { return hpAmountTotal; } }

    private void Awake()
    {
        ResetHP();
    }

    private void OnEnable()
    {
        ResetHP();
    }

    public void ResetHP()
    {
        currentHP = hpAmountTotal;
        OnHealthChanged?.Invoke(currentHP);
    }

    public void TakeHit(int damage)
    {
        currentHP -= damage;

        OnHealthChanged?.Invoke(currentHP);
        GameEvents.ShipHit(currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnHealthDepleted?.Invoke();
    }

    public void Heal(int hp)
    {
        currentHP += hp;
        if(currentHP > hpAmountTotal)
        {
            currentHP = hpAmountTotal;
        }

        OnHealthChanged?.Invoke(currentHP);
    }
}