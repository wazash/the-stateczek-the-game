using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpItem : ItemBase
{
    private int damageUpPrice = 10000;
    private int neededShopLvl = 2;

    private const string ITEM_NAME = "+1 dmg";

    private Bullet[] bullets;

    private void Start()
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

        base.InitItem();
    }

    public override void OnItemBuyButton()
    {
        base.OnItemBuyButton();

        AddDmgToLaser();

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
        itemPrice = damageUpPrice * itemLevel;

        base.UpdatePrice();
    }
    private void AddDmgToLaser()
    {
        if(bullets == null)
        {
            Debug.LogWarning("Bullets not found!");
            return;
        }

        foreach (Bullet bullet in bullets)
        {
            if (bullet.CompareTag("PlayerBullet"))
            {
                bullet.AddDmg(1);
            }
        }
    }

    private void GameEvents_OnGameStarted()
    {
        FindBullets();
    }

    private void FindBullets()
    {
        bullets = FindObjectsOfType<Bullet>(true);
    }
}
