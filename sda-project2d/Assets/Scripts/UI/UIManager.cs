using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Views
{
    Menu = 0,
    hud = 1,
    lose = 2,
    options = 3,
    shop = 4
}

public class UIManager : MonoBehaviour
{
    [Tooltip("0 - menuView, 1 - hudView, 2 - loseView, 3 - optionsView, 4 - shopView")]
    [SerializeField] private BaseView[] views;

    public static UIManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowView(Views name)
    {
        foreach (var view in views)
        {
            if (view.gameObject.activeSelf)
            {
                view.HideView();
            }
        }

        views[(int)name].ShowView();
    }

    public void ShowMainMenu()
    {
        foreach (var view in views)
        {
            if (view.gameObject.activeSelf)
            {
                view.HideView();
            }
        }
        
        views[0].ShowView();
    }

    public void ShowHUD()
    {
        foreach (var view in views)
        {
            if (view.gameObject.activeSelf)
            {
                view.HideView();
            }
        }

        views[1].ShowView();
    }

    public  void ShowLoseScreen()
    {
        foreach (var view in views)
        {
            if (view.gameObject.activeSelf)
            {
                view.HideView();
            }
        }

        views[2].ShowView();
    }

    public void ShowOptionsView()
    {
        foreach (var view in views)
        {
            if (view.gameObject.activeSelf)
            {
                view.HideView();
            }
        }

        views[3].ShowView();
    }
    public void ShowShopView()
    {
        foreach (var view in views)
        {
            if (view.gameObject.activeSelf)
            {
                view.HideView();
            }
        }

        views[4].ShowView();
    }
}
