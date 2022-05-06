using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public abstract class ItemBase : MonoBehaviour
{
    [SerializeField] protected TMP_Text itemPriceText, itemNameText;
    [SerializeField] protected Button itemButton;
    [SerializeField] protected Image itemImage, itemBorder;
    [SerializeField] protected GameObject soldGO, priceGO;
    protected int itemLevel = 1;
    protected int maxItemLevel = 3;
    protected int itemPrice = 0;

    protected bool isItemActive = false;

    public event Action OnItemBuy;

    private void Awake()
    {
        GameEvents.OnGameStarted += GameEvents_OnGameStarted;
    }
    private void OnDestroy()
    {
        GameEvents.OnGameStarted -= GameEvents_OnGameStarted;
    }
    private void GameEvents_OnGameStarted()
    {
        ResetItemLevel();
    }

    private void OnEnable()
    {
        InitItem();
    }

    public virtual void InitItem()
    {
        soldGO.SetActive(false);

        CheckIfEnoughShopLevel();

        if (isItemActive)
        {
            CheckIfEnoughMoney();
            CheckIfItemSold();
        }

    }
    public void CheckIfEnoughMoney()
    {
        if (ScoreManager.Instance != null)
        {
            if (ScoreManager.Instance.Score < itemPrice)
            {
                itemButton.interactable = false;
            }
            else
            {
                itemButton.interactable = true;
            }
        }
    }
    public void CheckIfEnoughShopLevel()
    {
        if (isItemActive)
        {
            ActiveItem();
        }
        else
        {
            DeactiveItem();
        }
    }
    protected void CheckIfItemSold()
    {
        if (itemLevel > maxItemLevel)
        {
            MakeItemSold();
        }
    }

    public virtual void OnItemBuyButton()
    {
        itemLevel++;
        ScoreManager.Instance.DecreaseScore(itemPrice);

        if (itemLevel > maxItemLevel)
        {
            MakeItemSold();
        }

        OnItemBuy?.Invoke();
    }

    protected void DeactiveItem()
    {
        itemNameText.text = "???";
        itemPriceText.text = "???";
        itemImage.color = Color.black;
        itemBorder.color = Color.black;
        itemButton.interactable = false;
        itemButton.GetComponent<Image>().color = Color.black;
    }
    protected virtual void ActiveItem()
    { 
        itemImage.color = Color.white;
        itemBorder.color = Color.white;
        itemButton.interactable = true;
        itemButton.GetComponent<Image>().color = Color.white;
    }

    protected virtual void UpdatePrice()
    {
        itemPriceText.text = $"$$ {itemPrice}";
    }

    protected void MakeItemSold()
    {
        soldGO.SetActive(true);
        DeactiveItem();
        priceGO.SetActive(false);
    }
    private void ResetItemLevel()
    {
        soldGO.SetActive(false);
        itemLevel = 1;
        priceGO.SetActive(true);
    }


}
