using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundsEffect sfx;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlayerBulletShooter.OnPlayerShot += PlayerBulletShooter_OnPlayerShoot;
        Enemy.OnEnemyShot += Enemy_OnEnemyShot;
        GameEvents.OnEnemyDied += GameEvents_OnEnemyDied;
        GameEvents.OnPlayerDied += GameEvents_OnPlayerDied;
        GameEvents.OnEnemyHit += GameEvents_OnEnemyHit;
        GameEvents.OnPowerupCollected += GameEvents_OnPowerupCollected;
    }



    private void OnDisable()
    {
        PlayerBulletShooter.OnPlayerShot -= PlayerBulletShooter_OnPlayerShoot;
        Enemy.OnEnemyShot -= Enemy_OnEnemyShot;
        GameEvents.OnEnemyDied -= GameEvents_OnEnemyDied;
        GameEvents.OnPlayerDied -= GameEvents_OnPlayerDied;
        GameEvents.OnEnemyHit -= GameEvents_OnEnemyHit;
    }

    private void Enemy_OnEnemyShot(Enemy enemy)
    {
        PlayEnemyShotSFX(enemy);
    }
    private void PlayEnemyShotSFX(Enemy enemy)
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        if(enemy.name == "EnemyShip_Hard(Clone)")
        {
            audioSource.PlayOneShot(sfx.shotSFX[1]);
        }
        else
        {
            audioSource.PlayOneShot(sfx.shotSFX[2]);
        }
    }

    private void PlayerBulletShooter_OnPlayerShoot()
    {
        PlayPlayerShotSFX();
    }
    private void PlayPlayerShotSFX()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(sfx.shotSFX[0]);
    }

    private void GameEvents_OnEnemyDied(Enemy obj)
    {
        PlayEnemyExplosion();
    }
    private void PlayEnemyExplosion()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(sfx.explosionSFX[1]);
    }

    private void GameEvents_OnPlayerDied(PlayerController obj)
    {
        PlayPlayerExplosionSFX();
    }
    private void PlayPlayerExplosionSFX()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(sfx.explosionSFX[0]);
    }

    private void GameEvents_OnEnemyHit(int currentHP)
    {
        PlayEnemyHitSFX(currentHP);
    }
    private void PlayEnemyHitSFX(int currentHP)
    {
        if(currentHP < 1)
        {
            return;
        }

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(sfx.hitSFX[1]);
    }
    private void GameEvents_OnPowerupCollected()
    {
        PlayPowerupCollected();
    }

    private void PlayPowerupCollected()
    {
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.PlayOneShot(sfx.powerupSFX);
    }
}
