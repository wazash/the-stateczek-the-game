using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager
{
    private int waveNumber = 0;
    EnemyWave currentWave;

    public EnemyWaveManager()
    {
        StartNextWave();
    }

    private void StartNextWave()
    {
        waveNumber += 1;

        currentWave = new EnemyWave(waveNumber);

        currentWave.OnWaveFinished += CurrentWave_OnWaveFinished;
    }

    private void CurrentWave_OnWaveFinished()
    {
        currentWave.OnWaveFinished -= CurrentWave_OnWaveFinished;

        StartNextWave();
    }

    public void UpdateWave()
    {
        if(currentWave == null)
        {
            return;
        }

        currentWave.UpdateWave();
    }
}
