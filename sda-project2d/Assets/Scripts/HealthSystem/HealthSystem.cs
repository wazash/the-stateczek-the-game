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
        currentHP = hpAmountTotal;
    }

    public void TakeHit(int damage)
    {
        currentHP -= damage;

        OnHealthChanged?.Invoke(currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnHealthDepleted?.Invoke();
    }
}