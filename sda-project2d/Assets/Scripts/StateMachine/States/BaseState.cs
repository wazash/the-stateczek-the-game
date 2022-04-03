public abstract class BaseState
{
    protected StateMachine myStateMachine;


    public virtual void EnterState(StateMachine stateMachine)
    {
        myStateMachine = stateMachine;
    }

    public virtual void UpdateState()

    {

    }

    public virtual void ExitState()
    {

    }
}