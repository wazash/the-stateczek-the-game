using UnityEngine;

public class GameState : BaseState
{
    private float currentTime;

    private bool gamePaused = false;

    private EnemyWaveManager enemyWaveManager;

    public override void EnterState(StateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        name = "GameState";

        enemyWaveManager = new EnemyWaveManager();

        Time.timeScale = 1;

        PlayerController.Instance.OnPlayerDied += PlayerInstance_OnPlayerDied;
        PlayerController.Instance.Respawn();

        GameEvents.OnGamePaused += GameEvents_OnGamePaused;

        CleanUpScene();

        EnemySpawner.Instance.ResetTimer();

        UIManager.Instance.ShowHUD();

        if(MusicManager.Instance.MusicSource.clip != MusicManager.Instance.Music.gameplayMusic)
        {
            MusicManager.Instance.PlayMusic(MusicManager.Instance.Music.gameplayMusic);
        }
    }



    public override void UpdateState()
    {
        base.UpdateState();

        CheckPauseButton();

        enemyWaveManager.UpdateWave();
    }

    public override void ExitState()
    {
        PlayerController.Instance.OnPlayerDied -= PlayerInstance_OnPlayerDied;
        GameEvents.OnGamePaused -= GameEvents_OnGamePaused;

        base.ExitState();

        Time.timeScale = 1;

        
    }
    public void CheckPauseButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }

            gamePaused = !gamePaused;

            GameEvents.GamePaused(this, gamePaused);
        }
    }

    private void PlayerInstance_OnPlayerDied()
    {
        myStateMachine.EnterState(new LoseState());
    }

    private static void CleanUpScene()
    {
        CleanUpEnemies();

        CleanUpBullets();

        CleanUpPowerups();
    }

    private static void CleanUpBullets()
    {
        var bullets = GameObject.FindObjectsOfType<Bullet>();
        foreach (Bullet bullet in bullets)
        {
            bullet.DestroyBullet();
        }
    }

    private static void CleanUpEnemies()
    {
        var enemies = GameObject.FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.DestroyEnemy();
        }
    }

    private static void CleanUpPowerups()
    {
        var powerups = GameObject.FindObjectsOfType<BasePowerup>();
        foreach (BasePowerup powerup in powerups)
        {
            powerup.DespawnPowerup();
        }
    }

    private void GameEvents_OnGamePaused(bool pauseState)
    {
        if (pauseState)
        {
            UIManager.Instance.ShowOptionsView();
        }
        else
        {
            UIManager.Instance.ShowHUD();
            gamePaused = pauseState;
        }
    }
}