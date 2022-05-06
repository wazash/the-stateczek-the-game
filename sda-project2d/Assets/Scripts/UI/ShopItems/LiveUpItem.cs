using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveUpItem : ItemBase
{
    private int liveUpPrice = 5000;
    private int neededShopLvl = 1;

    private const string ITEM_NAME = "+1 Live";

    private void Start()
    {
        OnItemBuy += LiveUpItem_OnItemBuy;
    }
    private void OnDestroy()
    {
        OnItemBuy -= LiveUpItem_OnItemBuy;
    }

    private void LiveUpItem_OnItemBuy()
    {
        CheckIfEnoughMoney();
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

        base.InitItem();
    }

    public override void OnItemBuyButton()
    {
        AddLive();
        UpdatePrice();
        base.OnItemBuyButton();
    }

    protected override void ActiveItem()
    {
        base.ActiveItem();
        itemNameText.text = ITEM_NAME;
        UpdatePrice();
    }

    private void AddLive()
    {
        PlayerController.Instance.HealthSystem.SetHealth(PlayerController.Instance.HealthSystem.HpAmountTotal + 1);
    }

    protected override void UpdatePrice()
    {
        itemPrice = liveUpPrice * itemLevel;
        
        base.UpdatePrice();
    }
}
