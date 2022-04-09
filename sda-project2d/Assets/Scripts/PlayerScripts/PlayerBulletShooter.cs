using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletShooter : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform[] bulletPositions;
    [SerializeField] private Bullet bulletPrefab;

    private void Update()
    {
        if (inputManager.ShootInput)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        foreach(var transformPosition in bulletPositions)
        {
            Bullet createdBullet = Instantiate<Bullet>(bulletPrefab, transformPosition.position, Quaternion.identity);

            createdBullet.Shoot(Vector3.right);
        }
    }
}
