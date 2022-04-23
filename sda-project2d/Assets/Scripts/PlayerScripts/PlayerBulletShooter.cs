using UnityEngine;

public class PlayerBulletShooter : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform[] bulletPositions;
    [SerializeField] private Bullet bulletPrefab;

    bool areWeaponsDisabled = true;

    ObjectPooler objectPooler;

    public static event System.Action OnPlayerShot;

    private void Awake()
    {
        playerController.OnPlayerDied += PlayerController_OnPlayerDied;
        playerController.OnPlayerRespawned += PlayerController_OnPlayerRespawned;
        GameEvents.OnGamePaused += GameEvents_OnGamePaused;
    }

    private void OnDestroy()
    {
        playerController.OnPlayerDied -= PlayerController_OnPlayerDied;
        playerController.OnPlayerRespawned -= PlayerController_OnPlayerRespawned;
        GameEvents.OnGamePaused -= GameEvents_OnGamePaused;
    }

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void Update()
    {
        if (areWeaponsDisabled)
        {
            return;
        }

        if (inputManager.ShootInput)
        {
            Shoot();
        }
    }

    private void GameEvents_OnGamePaused(bool pauseState)
    {
        areWeaponsDisabled = pauseState;
    }

    private void PlayerController_OnPlayerRespawned()
    {
        areWeaponsDisabled = false;
    }

    private void PlayerController_OnPlayerDied()
    {
        areWeaponsDisabled = true;
    }

    private void Shoot()
    {
        foreach (var transformPosition in bulletPositions)
        {
            //Bullet createdBullet = Instantiate<Bullet>(bulletPrefab, transformPosition.position, Quaternion.identity);
            GameObject createdBullet = objectPooler.SpawnFromPool("PlayerBullet", transformPosition.position, Quaternion.identity);
            
            createdBullet.GetComponent<Bullet>().Shoot(Vector3.right);
            OnPlayerShot?.Invoke();
        }
    }
}
