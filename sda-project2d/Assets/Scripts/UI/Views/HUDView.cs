using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDView : BaseView
{
    [SerializeField] private TMP_Text lifeCounter;


    public override void ShowView()
    {
        base.ShowView();

        PlayerController.Instance.HealthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        UpdateText(PlayerController.Instance.HealthSystem.CurrentHP);

    }
    public override void HideView()
    {
        base.HideView();

        PlayerController.Instance.HealthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(int obj)
    {
        UpdateText(obj);
    }

    private void UpdateText(int hpCount)
    {
        lifeCounter.text = $"Lives: {hpCount}";
    }
}
