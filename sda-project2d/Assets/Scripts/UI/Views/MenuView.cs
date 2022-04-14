using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : BaseView
{
    [SerializeField] private StateMachine gameStateMachine;

    public override void HideView()
    {
        base.HideView();
    }

    public override void ShowView()
    {
        base.ShowView();
    }

    public void OnStartGameButtonPressed()
    {
        gameStateMachine.EnterState(new GameState());
        HideView();
    }
    public void OnOptionsButtonPressed()
    {
        Debug.LogWarning("Nothing implemented here");
    }
    public void OnExitGameButtonPressed()
    {
        Debug.LogWarning("The game should shut down, but doesn't work in editor.");
        Application.Quit();
    }
}
