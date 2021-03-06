using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : BaseState
{
    public override void EnterState(StateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        name = StatesNames.MenuStateName;

        GameState.CleanUpScene();
        UIManager.Instance.ShowMainMenu();
        MusicManager.Instance.PlayMusic(MusicManager.Instance.Music.menuMusic);

        PlayerController.Instance.DisablePlayer();

        SoundsVolumeManager.Instance.SetLowpassValue(22000f);
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
