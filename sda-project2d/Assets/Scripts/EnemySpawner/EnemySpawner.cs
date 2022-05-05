using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    ObjectPooler objectPooler;

    [Header("Enemies prefabs")]
    [SerializeField] private Enemy[] enemyPrefabs;

    [Header("Spawn Interval")]
    [SerializeField] private float spawnInterval = 1.5f;
    public float SpawnInterval { get { return spawnInterval; } }
    private float minumumSpawnInterval = 0.3f;
    private float maximumSpawnInterval = 2.0f;

    [SerializeField] private float timeToIncreaseSpawnRatio = 2f;
    [SerializeField] private float amountToDecreaseSpawnInterval = 0.3f;

    [Header("Screen size")]
    private float leftXPosition, xPosition, bossPosition, yMin, yMax;

    [Header("Timer")]
    private float timer;
    public float Timer { get { return timer; } }
    private float intervalTimer;

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

        intervalTimer = 0;

        CalculateScreenSize();
    }

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        intervalTimer += Time.deltaTime;

        if(intervalTimer > timeToIncreaseSpawnRatio)
        {
            intervalTimer = 0;
            spawnInterval -= amountToDecreaseSpawnInterval;
            spawnInterval = Mathf.Clamp(spawnInterval, minumumSpawnInterval, maximumSpawnInterval);
        }
    }

    public Enemy SpawnEnemy(int waveNumber)
    {
        int randomShip = GetRandomID(waveNumber);

        var enemy = objectPooler.SpawnFromPool(enemyPrefabs[randomShip].name, new Vector3(xPosition, Random.Range(yMin, yMax), 0), Quaternion.identity);

        enemy.GetComponent<Enemy>().Initialize(leftXPosition);

        return enemy.GetComponent<Enemy>();

        #region old
        //if (timer < hardEnemiesSpawnStartTime)
        //{
        //    var enemy = objectPooler.SpawnFromPool(enemyPrefabs[0].name, new Vector3(xPosition, Random.Range(yMin, yMax), 0), Quaternion.identity);

        //    enemy.GetComponent<Enemy>().Initialize(leftXPosition);

        //    return enemy.GetComponent<Enemy>();
        //}
        //else if(timer > hardEnemiesSpawnStartTime && timer < sinusEnemiesSpawnStartTime)
        //{
        //    var enemy = objectPooler.SpawnFromPool(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)].name, new Vector3(xPosition, Random.Range(yMin, yMax), 0), Quaternion.identity);

        //    enemy.GetComponent<Enemy>().Initialize(leftXPosition);

        //    return enemy.GetComponent<Enemy>();
        //}
        //else
        //{
        //    var enemy = objectPooler.SpawnFromPool(enemyPrefabs[Random.Range(1, enemyPrefabs.Length)].name, new Vector3(xPosition, Random.Range(yMin, yMax), 0), Quaternion.identity);

        //    enemy.GetComponent<Enemy>().Initialize(leftXPosition);

        //    return enemy.GetComponent<Enemy>();
        //} 
        #endregion
    }

    public Boss SpawnBoss()
    {
        //var bossSpawned = Instantiate(bossPrefab, new Vector3(xPosition, 0, 0), Quaternion.identity);

        var bossSpawned = ObjectPooler.Instance.SpawnFromPool("Boss", new Vector3(bossPosition, 0, 0), Quaternion.identity);

        return bossSpawned.GetComponent<Boss>();
    }

    private int GetRandomID(int waveNumber)
    {
        int waveNumberClamp = Mathf.Clamp((waveNumber + 1) / 4, 0, enemyPrefabs.Length);

        int result = Random.Range(0, waveNumberClamp);

        return result;
    }

    public void ResetTimer()
    {
        timer = 0;
    }

    private void CalculateScreenSize()
    {
        var activeCamera = Camera.main;

        Vector3 bottomLeftPosition = activeCamera.ScreenToWorldPoint(Vector3.zero);
        Vector3 topRightPosition = activeCamera.ScreenToWorldPoint(new Vector3(activeCamera.pixelWidth, activeCamera.pixelHeight, 0));

        yMin = bottomLeftPosition.y;
        yMax = topRightPosition.y;

        leftXPosition = bottomLeftPosition.x;
        xPosition = topRightPosition.x - bottomLeftPosition.x;
        bossPosition = topRightPosition.x;
    }
}
