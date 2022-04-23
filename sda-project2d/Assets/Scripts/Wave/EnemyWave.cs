using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave
{
    public event System.Action OnWaveFinished;

    private float spawnInterval;
    private float currentTime;

    private int spawnedShips = 0, maxShips = 3;
    private int despawnedShips = 0;

    public EnemyWave()
    {
        spawnInterval = EnemySpawner.Instance.SpawnInterval;
        currentTime = spawnInterval;
    }
    public void UpdateWave()
    {
        if(spawnedShips >= maxShips)
        {
            return;
        }

        currentTime -= Time.deltaTime;

        if (currentTime < 0)
        {
            var enemy = EnemySpawner.Instance.SpawnEnemy();
            currentTime = spawnInterval;

            enemy.OnEnemyDespawned += Enemy_OnEnemyDespawned;

            spawnedShips++;

        }
    }

    private void Enemy_OnEnemyDespawned(Enemy enemy)
    {
        enemy.OnEnemyDespawned -= Enemy_OnEnemyDespawned;
        despawnedShips++;

        if(despawnedShips >= maxShips)
        {
            FinishWave();
        }
    }

    private void FinishWave()
    {
        OnWaveFinished?.Invoke();
    }
}
