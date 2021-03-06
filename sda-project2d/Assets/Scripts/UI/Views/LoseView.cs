using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoseView : BaseView
{
    [SerializeField] private StateMachine stateMachine;

    [SerializeField] private TMP_Text yourScoreText;
    [SerializeField] private TMP_Text highscoreText;

    [SerializeField] private GameObject defaultSelectetGameObject;

    public override void ShowView()
    {
        base.ShowView();

        GameEvents.OnScoreUpdated += GameEvents_OnScoreUpdated;
        GameEvents.OnHighscoreUpdated += GameEvents_OnHighscoreUpdated;

        PrintYourScore(ScoreManager.Instance.Score);
        PrintHighscore(ScoreManager.Instance.HighScore);

        EventSystem.current.SetSelectedGameObject(defaultSelectetGameObject);
    }

    public override void HideView()
    {
        base.HideView();

        GameEvents.OnHighscoreUpdated -= GameEvents_OnHighscoreUpdated;
    }

    private void GameEvents_OnScoreUpdated(int obj)
    {
        PrintYourScore(ScoreManager.Instance.Score);
    }
    private void GameEvents_OnHighscoreUpdated()
    {
        PrintHighscore(ScoreManager.Instance.HighScore);
    }

    private void PrintYourScore(int score)
    {
        yourScoreText.text = $"Your score:\n{score}";
    }

    private void PrintHighscore(int highscore)
    {
        highscoreText.text = $"Highscore:\n{highscore}";
    }

    public void OnMainMenuButtonPressed()
    {
        stateMachine.EnterState(new MenuState());
    }
}
