using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HealthSystem))]
public class Enemy : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Bullet bulletPrefab;

    [Header("Behaviour")]
    [SerializeField] private float speed;

    [Header("Shooting")]
    [SerializeField] private float minInterval, maxInterval;

    [Header("Other")]
    [SerializeField] private int pointsValue;

    private float timer = -1f;

    private float despawnPosition, despawnPositionOffset = 1;

    public int PointsValue { get { return pointsValue; } }

    public void Initialize(float leftScreenEdgePosition)
    {
        despawnPosition = leftScreenEdgePosition;
    }

    private void Awake()
    {
        healthSystem.OnHealthDepleted += HealthSystem_OnHealthDepleted;

        timer = Random.Range(minInterval, maxInterval);
    }

    private void OnDestroy()
    {
        healthSystem.OnHealthDepleted -= HealthSystem_OnHealthDepleted;
    }

    private void Update()
    {
        if(timer <= 0)
        {
            timer = Random.Range(minInterval, maxInterval);
            Shoot();
        }

        timer -= Time.deltaTime;
    }

    private void Shoot()
    {
        Bullet createdBullet = Instantiate<Bullet>(bulletPrefab, transform.position, Quaternion.identity);

        createdBullet.Shoot(Vector3.left);
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = Vector3.left * speed;
    }

    private void LateUpdate()
    {
        // Despawn enemy after gone offscreen
        if(transform.position.x < despawnPosition - despawnPositionOffset)
        {
            DestroyEnemy();
        }
    }

    // Enemy destroyed
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    // Enemy killed
    private void HealthSystem_OnHealthDepleted()
    {
        GameEvents.EnemyDied(this);

        DestroyEnemy();
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
