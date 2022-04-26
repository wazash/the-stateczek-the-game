using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Boss boss;
    [SerializeField] private Image bossHealthBar;

    private void OnEnable()
    {
        boss.HealthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }
    private void OnDisable()
    {
        boss.HealthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(int currentHP)
    {
        UpdateHealthbar(currentHP);
    }

    private void UpdateHealthbar(int hp)
    {
        bossHealthBar.fillAmount = (float)hp / boss.HealthSystem.HpAmountTotal;
    }
}
