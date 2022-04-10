using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [SerializeField] private Enemy enemyPrefab;
    private float leftXPosition, xPosition, yMin, yMax;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        var activeCamera = Camera.main;

        Vector3 bottomLeftPosition = activeCamera.ScreenToWorldPoint(Vector3.zero);
        Vector3 topRightPosition = activeCamera.ScreenToWorldPoint(new Vector3(activeCamera.pixelWidth, activeCamera.pixelHeight, 0));

        yMin = bottomLeftPosition.y;
        yMax = topRightPosition.y;

        leftXPosition = bottomLeftPosition.x;
        xPosition = topRightPosition.x - bottomLeftPosition.x;
    }

    public void SpawnEnemy()
    {
        var enemy = Instantiate<Enemy>(enemyPrefab, new Vector3(xPosition, Random.Range(yMin, yMax), 0), Quaternion.identity);

        enemy.Initialize(leftXPosition);
    }
}
