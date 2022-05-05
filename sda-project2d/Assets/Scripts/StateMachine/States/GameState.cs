using UnityEngine;

public class GameState : BaseState
{
    private float currentTime;

    private bool gamePaused = false;
    private bool canPasue = true;

    private EnemyWaveManager enemyWaveManager;

    public override void EnterState(StateMachine stateMachine)
    {
        Time.timeScale = 1;
        base.EnterState(stateMachine);

        name = StatesNames.GameStateName;
        enemyWaveManager = new EnemyWaveManager();

        PlayerController.Instance.OnPlayerDied += PlayerInstance_OnPlayerDied;
        GameEvents.OnGamePaused += GameEvents_OnGamePaused;
        GameEvents.OnShopOpened += GameEvents_OnShopOpened;
        GameEvents.OnShopClosed += GameEvents_OnShopClosed;


        PlayerController.Instance.Respawn();

        CleanUpScene();

        EnemySpawner.Instance.ResetTimer();


        UIManager.Instance.ShowHUD();

        ScoreManager.Instance.ResetScore();

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
        GameEvents.OnShopOpened -= GameEvents_OnShopOpened;
        GameEvents.OnShopClosed -= GameEvents_OnShopClosed;
        

        base.ExitState();

        Time.timeScale = 1;

        SoundsVolumeManager.Instance.SetLowpassValue(22000);
    }
    public void CheckPauseButton()
    {
        if (!canPasue)
        {
            return;
        }

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

    public static void CleanUpScene()
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
            //UIManager.Instance.ShowOptionsView();
            UIManager.Instance.ShowView(Views.options);
            SoundsVolumeManager.Instance.SetLowpassValue(1000f);
        }
        else
        {
            UIManager.Instance.ShowHUD();
            SoundsVolumeManager.Instance.SetLowpassValue(22000f);
            gamePaused = pauseState;
        }
    }

    private void GameEvents_OnShopOpened()
    {
        canPasue = false;
    }
    private void GameEvents_OnShopClosed()
    {
        canPasue = true;
    }
}