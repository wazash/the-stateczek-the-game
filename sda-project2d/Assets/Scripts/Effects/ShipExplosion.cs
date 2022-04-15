using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipExplosion : MonoBehaviour
{
    private ObjectPooler objectPooler;

    [SerializeField] private Animator explosion;
    [SerializeField] private float animationDuration = 1f;

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

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void GameEvents_OnPlayerDied(PlayerController obj)
    {
        PlayExplosion(obj.transform);
    }

    private void GameEvents_OnEnemyDied(Enemy obj)
    {
        PlayExplosion(obj.transform);
    }


    private void PlayExplosion(Transform transform)
    {
        //var explosion = Instantiate(this.explosion, position.position, Quaternion.identity);
        GameObject explosion = objectPooler.SpawnFromPool(this.explosion.gameObject.name, transform.position, Quaternion.identity);

        StartCoroutine(SetInactive(explosion));
    }


    private IEnumerator SetInactive(GameObject obj)
    {
        yield return new WaitForSeconds(animationDuration);
        obj.SetActive(false);
    }
}
