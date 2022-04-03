using UnityEngine;

public class DemoState : BaseState
{
    public override void EnterState(StateMachine stateMachine)
    {
        base.EnterState(stateMachine);

        Debug.Log("Demo State Enter");
    }

    public override void UpdateState()
    {
        base.UpdateState();

        Debug.Log("Demo State Update");
    }

    public override void ExitState()
    {
        base.ExitState();

        Debug.Log("Demo State Exit");
    }
}
