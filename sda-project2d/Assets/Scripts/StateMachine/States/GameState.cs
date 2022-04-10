using UnityEngine;

public class GameState : BaseState
{
    private float spawnInterval = 1.5f;
    private float currentTime;

    public override void EnterState(StateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        currentTime = spawnInterval;

        PlayerController.Instance.OnPlayerDied += PlayerInstance_OnPlayerDied;
        PlayerController.Instance.Respawn();

        CleanUpScene();

        UIManager.Instance.ShowHUD();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        currentTime -= Time.deltaTime;

        if (currentTime < 0)
        {
            EnemySpawner.Instance.SpawnEnemy();
            currentTime = spawnInterval;

        }
    }
    public override void ExitState()
    {
        PlayerController.Instance.OnPlayerDied -= PlayerInstance_OnPlayerDied;

        base.ExitState();
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