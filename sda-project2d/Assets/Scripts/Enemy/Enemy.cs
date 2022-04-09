using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;

    [SerializeField] private Bullet bulletPrefab;

    [SerializeField] private new Rigidbody2D rigidbody;

    [SerializeField] private float speed;

    private void Awake()
    {
        healthSystem.OnHealthDepleted += HealthSystem_OnHealthDepleted;
    }

    private void OnDestroy()
    {
        healthSystem.OnHealthDepleted -= HealthSystem_OnHealthDepleted;
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = Vector3.left * speed;
    }

    private void HealthSystem_OnHealthDepleted()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();

        if (healthSystem != null)
        {
            healthSystem.TakeHit(1);
            this.healthSystem.TakeHit(1);
        }
    }
}
