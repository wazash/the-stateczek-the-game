using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : BaseState
{
    public override void EnterState(StateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        name = StatesNames.LoseStateName;

        UIManager.Instance.ShowLoseScreen();

        GameEvents.HighscoreUpdated();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState(); 
    }
}
