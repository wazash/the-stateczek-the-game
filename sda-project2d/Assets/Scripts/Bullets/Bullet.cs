using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private float speed;

    [SerializeField] private float lifeLength;

    public void Shoot(Vector3 direction)
    {
        rigidbody.AddForce(direction * speed, ForceMode2D.Impulse);

        Destroy(gameObject, lifeLength);
    }

    public void DestroyBulletImmediate()
    {
        DestroyImmediate(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();

        if (healthSystem != null)
        {
            healthSystem.TakeHit(1);
        }

        DestroyBulletImmediate();
    }
}