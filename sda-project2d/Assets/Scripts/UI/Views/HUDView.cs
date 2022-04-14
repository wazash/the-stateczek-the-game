using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDView : BaseView
{
    [SerializeField] private TMP_Text lifeCounter;
    [SerializeField] private TMP_Text scoreCounter;

    [SerializeField] private Image[] lives;


    public override void ShowView()
    {
        base.ShowView();

        PlayerController.Instance.HealthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        UpdateHealthText(PlayerController.Instance.HealthSystem.CurrentHP);

        GameEvents.OnScoreUpdated += GameEvents_OnScoreUpdated;

        ScoreManager.Instance.ResetScore();
        GameEvents.ScoreUpdated(ScoreManager.Instance.Score);
    }

    public override void HideView()
    {
        base.HideView();

        PlayerController.Instance.HealthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;

        GameEvents.OnScoreUpdated -= GameEvents_OnScoreUpdated;
    }

    private void HealthSystem_OnHealthChanged(int obj)
    {
        UpdateHealthText(obj);
    }

    private void UpdateHealthText(int hpCount)
    {
        //lifeCounter.text = $"Lives: {hpCount}";
        for (int i = 0; i < lives.Length; i++)
        {
            if(i < hpCount)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }
        }
    }
    private void GameEvents_OnScoreUpdated(int score)
    {
        UpdateScoreText(score);
    }

    private void UpdateScoreText(int score)
    {
        scoreCounter.text = $"Score: {score}";
    }
}
