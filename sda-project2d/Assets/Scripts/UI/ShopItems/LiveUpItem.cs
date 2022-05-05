using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveUpItem : ItemBase
{
    private int liveUpPrice = 5000;
    private int neededShopLvl = 3;

    private const string ITEM_NAME = "+1 Live";

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
        base.OnItemBuyButton();
        AddLive();
        UpdatePrice();
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
