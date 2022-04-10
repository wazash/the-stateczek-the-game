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
        loseView.HideView();
        hudView.HideView();
    }

    public void ShowHUD()
    {
        hudView.ShowView();
        menuView.HideView();
        loseView.HideView();
    }

    public  void ShowLoseScreen()
    {
        loseView.ShowView();
        menuView.HideView();
        hudView.HideView();
    }
}
