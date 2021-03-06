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

    [SerializeField] GameObject pauseText;
    [SerializeField] private Image bossFillHP;

    public override void ShowView()
    {
        base.ShowView();

        PlayerController.Instance.HealthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        UpdateHealthText(PlayerController.Instance.HealthSystem.CurrentHP);

        GameEvents.OnScoreUpdated += GameEvents_OnScoreUpdated;
        //GameEvents.OnGamePaused += GameEvents_OnGamePaused;

        //ScoreManager.Instance.ResetScore();
        GameEvents.ScoreUpdated(ScoreManager.Instance.Score);

        pauseText.SetActive(false);
    }

    public override void HideView()
    {
        base.HideView();

        PlayerController.Instance.HealthSystem.OnHealthChanged -= HealthSystem_OnHealthChanged;

        GameEvents.OnScoreUpdated -= GameEvents_OnScoreUpdated;
        //GameEvents.OnGamePaused -= GameEvents_OnGamePaused;
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

    private void GameEvents_OnGamePaused(bool pauseState)
    {
        //pauseText.SetActive(pauseState);
        if (pauseState)
        {
            UIManager.Instance.ShowOptionsView();
        }
        else
        {
            UIManager.Instance.ShowHUD();
        }
    }
}
