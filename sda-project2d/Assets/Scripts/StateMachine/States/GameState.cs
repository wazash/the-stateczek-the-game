using UnityEngine;

public class GameState : BaseState
{
    private float currentTime;

    bool gamePaused = false;

    public override void EnterState(StateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        Time.timeScale = 1;

        currentTime = EnemySpawner.Instance.SpawnInterval;

        PlayerController.Instance.OnPlayerDied += PlayerInstance_OnPlayerDied;
        PlayerController.Instance.Respawn();

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

        currentTime -= Time.deltaTime;

        if (currentTime < 0)
        {
            EnemySpawner.Instance.SpawnEnemy();
            currentTime = EnemySpawner.Instance.SpawnInterval;
        }
    }

    public override void ExitState()
    {
        PlayerController.Instance.OnPlayerDied -= PlayerInstance_OnPlayerDied;

        base.ExitState();

        Time.timeScale = 1;
    }
    private void CheckPauseButton()
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
}