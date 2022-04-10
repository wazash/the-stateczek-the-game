using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private BaseView loseView, hudView, menuView;

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

    public void ShowMainMenu()
    {
        menuView.ShowView();
    }
    public void HideMainMenu()
    {
        menuView.HideView();
    }

    public void ShowHUD()
    {
        hudView.ShowView();
    }
    public void HideHUD()
    {
        hudView.HideView();
    }

    public  void ShowLoseScreen()
    {
        loseView.ShowView();
    }
    public void HideLoseScreen()
    {
        loseView.HideView();
    }
}
