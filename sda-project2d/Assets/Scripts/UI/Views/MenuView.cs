using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuView : BaseView
{
    [SerializeField] private StateMachine gameStateMachine;

    [SerializeField] private GameObject defaultSelectetGameObject;

    public override void HideView()
    {
        base.HideView();
    }

    public override void ShowView()
    {
        base.ShowView();

        EventSystem.current.SetSelectedGameObject(defaultSelectetGameObject);
    }

    public void OnStartGameButtonPressed()
    {
        gameStateMachine.EnterState(new GameState());
        HideView();
    }
    public void OnOptionsButtonPressed()
    {
        UIManager.Instance.ShowOptionsView();
    }
    public void OnExitGameButtonPressed()
    {
        Debug.LogWarning("The game should shut down, but doesn't work in editor.");
        Application.Quit();
    }
    
}
