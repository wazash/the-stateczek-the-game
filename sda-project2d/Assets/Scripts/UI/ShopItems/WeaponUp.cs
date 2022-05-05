using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUp : ItemBase
{
    private int weaponUpPrice = 5000;
    private int neededShopLvl = 2;

    private PlayerBulletShooter shooter;

    private const string ITEM_NAME = "+1 laser";

    private void Start()
    {
        shooter = FindObjectOfType<PlayerBulletShooter>();

        maxItemLevel = 2;
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
        base.OnItemBuyButton();

        SetWeapons(itemLevel);

        UpdatePrice();

        InitItem();
    }

    protected override void ActiveItem()
    {
        base.ActiveItem();
        itemNameText.text = ITEM_NAME;
        UpdatePrice();
    }

    protected override void UpdatePrice()
    {
        itemPrice = weaponUpPrice * itemLevel;

        base.UpdatePrice();
    }

    private void SetWeapons(int configurationNumber)
    {
        shooter.SetShootPositions(configurationNumber);
    }
}
