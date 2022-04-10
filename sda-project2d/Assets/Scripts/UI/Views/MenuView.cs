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
}
