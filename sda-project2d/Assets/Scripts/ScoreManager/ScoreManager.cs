using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private int score;
    private int highScore;

    public int Score { get { return score; } }
    public int HighScore { get { return highScore; } }

    private const string HIGHSCORE_KEY = "highscore";

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = GetComponent<ScoreManager>();
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        GameEvents.OnEnemyDied += GameEvents_OnEnemyDied;
        GameEvents.OnHighscoreUpdated += GameEvents_OnHighscoreUpdated;

        // Loading highscore from PlayerPrefs
        highScore = LoadHighscore();
    }

    private void OnDestroy()
    {
        GameEvents.OnEnemyDied -= GameEvents_OnEnemyDied;
        GameEvents.OnHighscoreUpdated -= GameEvents_OnHighscoreUpdated;
    }

    private void GameEvents_OnEnemyDied(Enemy enemy)
    {
        score += enemy.PointsValue;

        GameEvents.ScoreUpdated(score);
        //GameEvents.HighscoreUpdated();
    }

    private void GameEvents_OnHighscoreUpdated()
    {
        if (score > highScore)
        {
            highScore = score;
            SaveHighscore();

            Debug.Log($"New Highscore: {highScore}");
        }
    }
    public void ResetScore()
    {
        score = 0;
    }

    private void SaveHighscore()
    {
        PlayerPrefs.SetInt(HIGHSCORE_KEY, highScore);
    }
    private int LoadHighscore()
    {
        return PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);
    }
    public void ResetHighscore()
    {
        highScore = 0;
        PlayerPrefs.SetInt(HIGHSCORE_KEY, highScore);
    }
}
