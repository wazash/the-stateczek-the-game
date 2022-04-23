using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsSpawner : MonoBehaviour
{
    //[SerializeField] private GameObject healthPowerupPrefab;

    private const string HEALTH_POWERUP = "Health_Powerup";

    void Start()
    {
        GameEvents.OnEnemyDied += GameEvents_OnEnemyDied;
    }

    private void GameEvents_OnEnemyDied(Enemy obj)
    {
        var randomFloat = Random.Range(1f, 100f);

        if(randomFloat <= 33f)
        {
            SpawnHealthPowerup(obj.transform.position);
        }
    }

    private void SpawnHealthPowerup(Vector3 position)
    {
        ObjectPooler.Instance.SpawnFromPool(HEALTH_POWERUP, position, Quaternion.identity);
    }
}
