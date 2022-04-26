using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : BaseState
{
    public override void EnterState(StateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        name = "MenuState";

        UIManager.Instance.ShowMainMenu();
        MusicManager.Instance.PlayMusic(MusicManager.Instance.Music.menuMusic);

        PlayerController.Instance.DisablePlayer();
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
