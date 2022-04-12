using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipExplosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionPS;

    private void Awake()
    {
        GameEvents.OnEnemyDied += GameEvents_OnEnemyDied;
        GameEvents.OnPlayerDied += GameEvents_OnPlayerDied;
    }

    private void GameEvents_OnPlayerDied(PlayerController obj)
    {
        PlayExplosion(obj.transform);
    }

    private void GameEvents_OnEnemyDied(Enemy obj)
    {
        PlayExplosion(obj.transform);
    }

    private void OnDestroy()
    {
        GameEvents.OnEnemyDied -= GameEvents_OnEnemyDied;
        GameEvents.OnPlayerDied -= GameEvents_OnPlayerDied;
    }

    private void PlayExplosion(Transform position)
    {
        var explosion = Instantiate(explosionPS, position.position,Quaternion.identity);
        explosion.Play();

        Destroy(explosion.gameObject, 1);
    }
}