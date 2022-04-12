using System;

public class GameEvents
{
    public static event Action<Enemy> OnEnemyDied;
    public static void EnemyDied(Enemy enemy)
    {
        if (enemy == null)
        {
            return;
        }

        OnEnemyDied?.Invoke(enemy);
    }

    public static event Action<PlayerController> OnPlayerDied;
    public static void PlayerDied(PlayerController player)
    {
        if(player == null)
        {
            return;
        }

        OnPlayerDied?.Invoke(player);
    }

    public static event Action<int> OnScoreUpdated;
    public static void ScoreUpdated(int score)
    {
        OnScoreUpdated?.Invoke(score);
    }

    public static event Action OnHighscoreUpdated;
    public static void HighscoreUpdated()
    {
        OnHighscoreUpdated?.Invoke();
    }
}
