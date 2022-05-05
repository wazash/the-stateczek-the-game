using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillsUpItem : ItemBase
{
    private int pillsUpPirce = 1000;
    private int neededShopLvl = 1;

    private string itemName;

    private HealthRestorePowerup[] pills;
    [SerializeField] private PowerupsSpawner powerupsSpawner;

    private void Awake()
    {
        GameEvents.OnGameStarted += GameEvents_OnGameStarted;
    }
    private void OnDestroy()
    {
        GameEvents.OnGameStarted -= GameEvents_OnGameStarted;
    }

    public override void InitItem()
    {
        UpdatePrice();

        if (ShopView.shopShowedUp >= neededShopLvl)
        {
            isItemActive = true;
        }
        else
        {
            isItemActive = false;
        }

        if(itemLevel == 2)
        {
            itemName = "More Drop";
        }
        if(itemLevel == 1 || itemLevel == 3)
        {
            itemName = "+1 Heal";
        }

        base.InitItem();
    }

    public override void OnItemBuyButton()
    {
        
        switch (itemLevel)
        {
            case 1:
                UpgradeHealValue(2);
                break;
            case 2:
                UpgradeSpawnFrequence(20f);
                break;
            case 3:
                UpgradeHealValue(3);
                break;
        }

        base.OnItemBuyButton();
        UpdatePrice();
        InitItem();
    }

    protected override void ActiveItem()
    {
        base.ActiveItem();

        itemNameText.text = itemName;
        UpdatePrice();
    }

    protected override void UpdatePrice()
    {
        itemPrice = pillsUpPirce * itemLevel;

        base.UpdatePrice();
    }

    private void UpgradeHealValue(int value)
    {
        if(pills == null)
        {
            Debug.LogWarning("Pills not found!");
            return;
        }

        foreach (HealthRestorePowerup pill in pills)
        {
            pill.ChangeHealAmount(value);
        }
    }

    private void UpgradeSpawnFrequence(float value)
    {
        powerupsSpawner.ChangeDropProbability(value);
    }

    private void GameEvents_OnGameStarted()
    {
        FindPills();
    }

    private void FindPills()
    {
        pills = FindObjectsOfType<HealthRestorePowerup>(true);
    }

}
