using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score;

    private void Awake()
    {
        GameEvents.OnEnemyDied += GameEvents_OnEnemyDied;
    }

    private void OnDestroy()
    {
        GameEvents.OnEnemyDied -= GameEvents_OnEnemyDied;
    }

    private void GameEvents_OnEnemyDied(Enemy obj)
    {
        score += 100;

        GameEvents.ScoreUpdated(score);
    }
}
