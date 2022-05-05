using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private float speed;

    [SerializeField] private float lifeLength;

    private int currentDmg = 1;
    public int CurrentDmg { get { return currentDmg; } }

    private void Start()
    {
        GameEvents.OnGameStarted += GameEvents_OnGameStarted;
    }
    private void OnDestroy()
    {
        GameEvents.OnGameStarted -= GameEvents_OnGameStarted;
    }

    private void GameEvents_OnGameStarted()
    {
        ResetDmg();
    }

    public void Shoot(Vector3 direction)
    {
        rigidbody.AddForce(direction * speed, ForceMode2D.Impulse);

        //Destroy(gameObject, lifeLength);
        Invoke(nameof(DestroyBullet), lifeLength);
    }

    public void DestroyBullet()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();

        if (healthSystem != null)
        {
            healthSystem.TakeHit(currentDmg);
        }

        DestroyBullet();
    }

    public void AddDmg(int value)
    {
        currentDmg += value;
    }

    public void ResetDmg()
    {
        currentDmg = 1;
    }
}