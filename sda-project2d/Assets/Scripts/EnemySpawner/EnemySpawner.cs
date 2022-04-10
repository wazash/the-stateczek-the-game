using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;

    float leftXPosition, xPosition, yMin, yMax;

    private void Awake()
    {
        var activeCamera = Camera.main;

        Vector3 bottomLeftPosition = activeCamera.ScreenToWorldPoint(Vector3.zero);
        Vector3 topRightPosition = activeCamera.ScreenToWorldPoint(new Vector3(activeCamera.pixelWidth, activeCamera.pixelHeight, 0));

        yMin = bottomLeftPosition.y;
        yMax = topRightPosition.y;

        leftXPosition = bottomLeftPosition.x;
        xPosition = topRightPosition.x - bottomLeftPosition.x;

        TestSpawn();
    }

    private void TestSpawn()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        var enemy = Instantiate<Enemy>(enemyPrefab, new Vector3(xPosition, Random.Range(yMin, yMax), 0), Quaternion.identity);

        enemy.Initialize(leftXPosition);
    }
}
