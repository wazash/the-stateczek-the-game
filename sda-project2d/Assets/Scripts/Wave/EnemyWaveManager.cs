using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager
{
    private int waveNumber = 0;
    EnemyWave currentWave;

    public static event System.Action OnNextWave;
    public static void NextWave()
    {
        if(OnNextWave == null)
        {
            return;
        }

        OnNextWave?.Invoke();
    }

    public EnemyWaveManager()
    {
        OnNextWave += StartNextWave;

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

        if (currentWave.isBossWave)
        {
            UIManager.Instance.ShowView(Views.shop);
        }
        else
        {
            StartNextWave();
        }
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
