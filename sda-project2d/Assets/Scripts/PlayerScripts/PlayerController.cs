using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public event System.Action OnPlayerRespawned;
    public event System.Action OnPlayerDied;

    [SerializeField] private InputManager inputManager;
    [SerializeField] private new Rigidbody2D rigidbody;

    [SerializeField] private HealthSystem healthSystem;

    [SerializeField] private GameObject playerSprite;
    [SerializeField] private Collider2D[] colliders;

    [SerializeField] private float speed;

    private Camera activeCamera;
    private Rect cameraBounds;

    private Vector3 spawnPosition;
    private bool isPlayerDead = true;

    public HealthSystem HealthSystem { get { return healthSystem; } }

    public void Respawn()
    {
        transform.position = spawnPosition;
        healthSystem.ResetHP();

        playerSprite.SetActive(true);

        isPlayerDead = false;

        SwitchPlayerCollider(true);

        OnPlayerRespawned?.Invoke();
    }

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

        spawnPosition = transform.position;
    }

    private void Start()
    {
        activeCamera = Camera.main;

        // Get left bottom camera corner
        Vector3 bottomLeftPosition = activeCamera.ScreenToWorldPoint(Vector3.zero);
        // Get get right top camera corner
        Vector3 topRightPosition = activeCamera.ScreenToWorldPoint(new Vector3(activeCamera.pixelWidth, activeCamera.pixelHeight, 0));

        cameraBounds = new Rect(
            bottomLeftPosition.x,
            bottomLeftPosition.y,
            topRightPosition.x - bottomLeftPosition.x,
            topRightPosition.y - bottomLeftPosition.y);

        //healthSystem.OnHealthDepleted += OnHealthDepleted;
        healthSystem.OnHealthDepleted += HealthSystem_OnHealthDepleted;
    }

    private void FixedUpdate()
    {
        if (isPlayerDead)
        {
            return;
        }

        Vector2 movementVector = new Vector2(
            inputManager.HorizontalInput * speed,
            inputManager.VerticalInput * speed);

        rigidbody.AddForce(movementVector);
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, cameraBounds.xMin, cameraBounds.xMax),
            Mathf.Clamp(transform.position.y, cameraBounds.yMin, cameraBounds.yMax),
            transform.position.z);
    }

    private void OnDestroy()
    {
        healthSystem.OnHealthDepleted -= HealthSystem_OnHealthDepleted;
    }

    private void HealthSystem_OnHealthDepleted()
    {
        GameEvents.PlayerDied(this);

        OnPlayerDied?.Invoke();

        DisablePlayer();
    }

    private void DisablePlayer()
    {
        playerSprite.SetActive(false);
        isPlayerDead = true;
        SwitchPlayerCollider(false);
    }

    private void SwitchPlayerCollider(bool value)
    {
        foreach (var collider in colliders)
        {
            collider.enabled = value;
        }
    }

}