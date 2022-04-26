using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Tooltip("0 - menuView, 1 - hudView, 2 - loseView")]
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

    public void ShowMainMenu()
    {
        foreach (var view in views)
        {
            view.HideView();
        }
        
        views[0].ShowView();
    }

    public void ShowHUD()
    {
        foreach (var view in views)
        {
            view.HideView();
        }

        views[1].ShowView();
    }

    public  void ShowLoseScreen()
    {
        foreach (var view in views)
        {
            view.HideView();
        }

        views[2].ShowView();
    }

    public void ShowOptionsView()
    {
        foreach (var view in views)
        {
            view.HideView();
        }

        views[3].ShowView();
    }
}
