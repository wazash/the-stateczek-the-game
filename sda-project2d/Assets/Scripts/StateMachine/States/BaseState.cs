public abstract class BaseState
{
    protected StateMachine myStateMachine;
    protected string name;
    public string Name { get { return name; } }

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