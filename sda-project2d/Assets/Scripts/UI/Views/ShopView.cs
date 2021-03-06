using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopView : BaseView
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private Button nextWaveButton;

    [SerializeField] private ItemBase[] items;

    [SerializeField] private GameObject defaultSelectetGameObject;

    public static int shopShowedUp;

    private void Start()
    {
        ResetShopLevel();

        nextWaveButton.onClick.RemoveAllListeners();
        nextWaveButton.onClick.AddListener(OnNextWaveButtonClicked);

        GameEvents.OnGameStarted += GameEvents_OnGameStarted;
        
        foreach (var item in items)
        {
            item.OnItemBuy += UpdateMoneyText;
            item.OnItemBuy += InitItems;
        }
    }

    private void OnDestroy()
    {
        nextWaveButton.onClick.RemoveAllListeners();

        GameEvents.OnGameStarted -= GameEvents_OnGameStarted;

        foreach (var item in items)
        {
            item.OnItemBuy -= UpdateMoneyText;
        }
    }

    public override void ShowView()
    {
        base.ShowView();

        EventSystem.current.SetSelectedGameObject(defaultSelectetGameObject);

        shopShowedUp++;
        GameEvents.ShopOpened();

        PlayerController.Instance.DisablePlayer();

        UpdateMoneyText();
    }
    public override void HideView()
    {
        base.HideView();

        GameEvents.ShopClosed();

        PlayerController.Instance.EnablePlayer();
    }

    public void OnNextWaveButtonClicked()
    {
        UIManager.Instance.ShowView(Views.hud);
        EnemyWaveManager.NextWave();
    }

    public void ResetShopLevel()
    {
        shopShowedUp = 1;
    }

    private void GameEvents_OnGameStarted()
    {
        ResetShopLevel();
    }

    private void UpdateMoneyText()
    {
        moneyText.text = $"$$ {ScoreManager.Instance.Score}";
    }
    private void InitItems()
    {
        foreach (ItemBase item in items)
        {
            //item.CheckIfEnoughShopLevel();
            item.CheckIfEnoughMoney();
        }
    }
}
