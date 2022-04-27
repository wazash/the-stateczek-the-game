using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    private Bullet bullet;

    private void Start()
    {
        bullet = GetComponentInParent<Bullet>();
    }
    private void OnBecameInvisible()
    {
        bullet.DestroyBullet();
    }
}
