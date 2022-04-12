using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [SerializeField] private Enemy[] enemyPrefabs;
    private float leftXPosition, xPosition, yMin, yMax;

    [Tooltip("Time in second, after which stronger enemies will spawn")]
    [SerializeField] private float hardEnemiesSpawnStartTime = 30.0f;
    private float timer;
    public float Timer { get { return timer; } }

    

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

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void SpawnEnemy()
    {
        if (timer < hardEnemiesSpawnStartTime)
        {
            var enemy = Instantiate<Enemy>(enemyPrefabs[0], new Vector3(xPosition, Random.Range(yMin, yMax), 0), Quaternion.identity);

            enemy.Initialize(leftXPosition);
        }
        else
        {
            var enemy = Instantiate<Enemy>(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], new Vector3(xPosition, Random.Range(yMin, yMax), 0), Quaternion.identity);

            enemy.Initialize(leftXPosition);
        }
    }

    public void ResetTimer()
    {
        timer = 0;
    }
}
