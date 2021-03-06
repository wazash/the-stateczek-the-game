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
        playerController.OnPlayerDisabled += PlayerController_OnPlayerDisabled;
        playerController.OnPlayerEnabled += PlayerController_OnPlayerEnabled;
        GameEvents.OnGamePaused += GameEvents_OnGamePaused;
        GameEvents.OnGameStarted += GameEvents_OnGameStarted;
    }



    private void OnDestroy()
    {
        playerController.OnPlayerDied -= PlayerController_OnPlayerDied;
        playerController.OnPlayerRespawned -= PlayerController_OnPlayerRespawned;
        playerController.OnPlayerDisabled -= PlayerController_OnPlayerDisabled;
        playerController.OnPlayerEnabled -= PlayerController_OnPlayerEnabled;
        GameEvents.OnGamePaused -= GameEvents_OnGamePaused;
        GameEvents.OnGameStarted -= GameEvents_OnGameStarted;
    }

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;

        SetShootPositions(1);
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
    private void GameEvents_OnGameStarted()
    {
        SetShootPositions(1);
    }

    private void PlayerController_OnPlayerRespawned()
    {
        areWeaponsDisabled = false;
    }

    private void PlayerController_OnPlayerDied()
    {
        areWeaponsDisabled = true;
    }
    private void PlayerController_OnPlayerDisabled()
    {
        areWeaponsDisabled = true;
    }
    private void PlayerController_OnPlayerEnabled()
    {
        areWeaponsDisabled = false;
    }

    private void Shoot()
    {
        foreach (var transformPosition in bulletPositions)
        {
            if (transformPosition.gameObject.activeSelf)
            {
                //Bullet createdBullet = Instantiate<Bullet>(bulletPrefab, transformPosition.position, Quaternion.identity);
                GameObject createdBullet = objectPooler.SpawnFromPool("PlayerBullet", transformPosition.position, Quaternion.identity);

                createdBullet.GetComponent<Bullet>().Shoot(Vector3.right);
                OnPlayerShot?.Invoke();
            }

        }
    }

    public void SetShootPositions(int level)
    {
        switch (level)
        {
            case 1:
                bulletPositions[0].gameObject.SetActive(true);
                bulletPositions[1].gameObject.SetActive(false);
                bulletPositions[2].gameObject.SetActive(false);
                break;
            case 2:
                bulletPositions[0].gameObject.SetActive(false);
                bulletPositions[1].gameObject.SetActive(true);
                bulletPositions[2].gameObject.SetActive(true);
                break;
            case 3:
                bulletPositions[0].gameObject.SetActive(true);
                bulletPositions[1].gameObject.SetActive(true);
                bulletPositions[2].gameObject.SetActive(true);
                break;
            default:
                Debug.LogWarning("Configration not found!");
                break;
        }
    }
}
