using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipExplosion : MonoBehaviour
{
    [SerializeField] private Animator explosion;

    private void Awake()
    {
        GameEvents.OnEnemyDied += GameEvents_OnEnemyDied;
        GameEvents.OnPlayerDied += GameEvents_OnPlayerDied;
    }
    private void OnDestroy()
    {
        GameEvents.OnEnemyDied -= GameEvents_OnEnemyDied;
        GameEvents.OnPlayerDied -= GameEvents_OnPlayerDied;
    }

    private void GameEvents_OnPlayerDied(PlayerController obj)
    {
        PlayExplosion(obj.transform);
    }

    private void GameEvents_OnEnemyDied(Enemy obj)
    {
        PlayExplosion(obj.transform);
    }


    private void PlayExplosion(Transform position)
    {

        var explosion = Instantiate(this.explosion, position.position, Quaternion.identity);

        Destroy(explosion.gameObject, explosion.GetCurrentAnimatorStateInfo(0).length);
    }
}
