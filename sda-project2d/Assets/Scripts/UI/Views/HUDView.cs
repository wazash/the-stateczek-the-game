using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDView : BaseView
{
    [SerializeField] private TMP_Text lifeCounter;
    [SerializeField] private TMP_Text scoreCounter;


    public override void ShowView()
    {
        base.ShowView();

        PlayerController.Instance.HealthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        UpdateText(PlayerController.Instance.HealthSystem.CurrentHP);

        GameEvents.OnScoreUpdated += GameEvents_OnScoreUpdated;

    }

    public override void HideView()
    {
        base.HideView();

        PlayerController.Instance.HealthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;

        GameEvents.OnScoreUpdated -= GameEvents_OnScoreUpdated;
    }

    private void HealthSystem_OnHealthChanged(int obj)
    {
        UpdateText(obj);
    }

    private void GameEvents_OnScoreUpdated(int obj)
    {
        scoreCounter.text = obj.ToString();
    }
    private void UpdateText(int hpCount)
    {
        lifeCounter.text = $"Lives: {hpCount}";
    }
}
