using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private Transform[] shootingPositions; 

    private float leftXPosition, xPosition, yMin, yMax;

    private bool movingUp = true;

    private void Start()
    {
        var activeCamera = Camera.main;

        Vector3 bottomLeftPosition = activeCamera.ScreenToWorldPoint(Vector3.zero);
        Vector3 topRightPosition = activeCamera.ScreenToWorldPoint(new Vector3(activeCamera.pixelWidth, activeCamera.pixelHeight, 0));

        yMin = bottomLeftPosition.y;
        yMax = topRightPosition.y;

        leftXPosition = bottomLeftPosition.x;
        xPosition = topRightPosition.x;

        transform.position = new Vector3(xPosition, 0, 0);
    }

    protected override void Move()
    {
        if (movingUp)
        {
            rigidbody.velocity = Vector2.up * speed;
        }
        else
        {
            rigidbody.velocity = Vector2.down * speed;
        }

        if(transform.position.y > yMax)
        {
            movingUp = false;
        }
        if(transform.position.y < yMin)
        {
            movingUp = true;
        }
    }

    protected override void Shoot()
    {
        foreach (var transformPosition in shootingPositions)
        {
            //Bullet createdBullet = Instantiate<Bullet>(bulletPrefab, transformPosition.position, Quaternion.identity);
            GameObject createdBullet = ObjectPooler.Instance.SpawnFromPool("EnemyBullet_1", transformPosition.position, Quaternion.identity);

            createdBullet.GetComponent<Bullet>().Shoot(Vector3.left);
            //OnEnemyShot?.Invoke(this);
        }
    }
}
