using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInit : MonoBehaviour
{
    private PlayerController player;

    [SerializeField] private int startHP = 3;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    private void Start()
    {
        GameEvents.OnGameStarted += GameEvents_OnGameStarted;
    }
    private void OnDestroy()
    {
        GameEvents.OnGameStarted -= GameEvents_OnGameStarted;
    }

    #region GameEvents methods
    private void GameEvents_OnGameStarted()
    {
        PlayerInitNewGame();
    }

    #endregion

    private void PlayerInitNewGame()
    {
        player.HealthSystem.SetHealth(startHP);
    }
}
