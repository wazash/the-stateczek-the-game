using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave
{
    public event System.Action OnWaveFinished;

    private int waveIdentifier;

    private float spawnInterval;
    private float currentTime;

    private int spawnedShips = 0, maxShips = 5;
    private int despawnedShips = 0;

    private int whatWaveBoss = 5;
    private int bossHealthMultiplier = 5;

    public EnemyWave(int waveNumber)
    {
        waveIdentifier = waveNumber;
        spawnInterval = EnemySpawner.Instance.SpawnInterval;
        currentTime = spawnInterval;

        maxShips = waveNumber * 2;
    }
    public void UpdateWave()
    {
        if(spawnedShips >= maxShips)
        {
            return;
        }

        if(waveIdentifier % whatWaveBoss == 0)
        {
            BossWave();
        }
        else
        {
            NormalWave();
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

    private void NormalWave()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 0)
        {
            var enemy = EnemySpawner.Instance.SpawnEnemy(waveIdentifier);
            currentTime = spawnInterval;

            enemy.OnEnemyDespawned += Enemy_OnEnemyDespawned;

            spawnedShips++;

        }
    }

    private void BossWave()
    {
        var boss = EnemySpawner.Instance.SpawnBoss();

        maxShips = 1;
        boss.OnEnemyDespawned += Enemy_OnEnemyDespawned;

        boss.HealthSystem.SetHealth(waveIdentifier * bossHealthMultiplier);

        spawnedShips++;
    }
}
