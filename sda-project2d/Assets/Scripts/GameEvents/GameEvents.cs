using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents
{
    public static event System.Action<Enemy> OnEnemyDied;
    public static void EnemyDied(Enemy enemy)
    {
        if(enemy == null)
        {
            return;
        }

        OnEnemyDied?.Invoke(enemy);
    }

    public static event System.Action<int> OnScoreUpdated;
    public static void ScoreUpdated(int score)
    {
        OnScoreUpdated?.Invoke(score);
    }
}
