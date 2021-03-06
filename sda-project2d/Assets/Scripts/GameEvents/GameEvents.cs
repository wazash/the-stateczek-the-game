using System;
using UnityEngine;

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

    public static event Action<int> OnEnemyHit;
    public static void ShipHit(int currentHP)
    {
        OnEnemyHit?.Invoke(currentHP);
    }

    public static event Action<bool> OnGamePaused;
    public static void GamePaused(GameState gameState, bool pauseState)
    {
        if(gameState == null)
        {
            return;
        }

        OnGamePaused?.Invoke(pauseState);
    }

    public static event Action OnPowerupCollected;
    public static void PowerupCollected()
    {
        OnPowerupCollected?.Invoke();
    }

    public static event Action OnGameStarted;
    public static void GameStarted()
    {
        if(OnGameStarted == null)
        {
            return;
        }
        OnGameStarted?.Invoke();
    }

    public static event Action OnShopOpened;
    public static void ShopOpened()
    {
        if(OnShopOpened == null)
        {
            return;
        }

        OnShopOpened?.Invoke();
    }
    public static event Action OnShopClosed;
    public static void ShopClosed()
    {
        if (OnShopOpened == null)
        {
            return;
        }

        OnShopClosed?.Invoke();
    }
}
