using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : BaseState
{
    public override void EnterState(StateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        UIManager.Instance.ShowLoseScreen();
    }

    public override void ExitState()
    {
        base.ExitState();

        UIManager.Instance.HideLoseScreen();
    }

    public override void UpdateState()
    {
        base.UpdateState(); 
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            myStateMachine.EnterState(new GameState());
        }
    }
}
