using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private BaseState currentState;
    public BaseState CurrentState { get { return currentState; } }

    private void Start()
    {
        EnterState(new MenuState());
    }

    private void Update()
    {
        currentState.UpdateState();
    }

    private void OnDestroy()
    {
        currentState.ExitState();
    }

    public void EnterState(BaseState stateToEnter)
    {
        currentState?.ExitState();

        currentState = stateToEnter;

        currentState?.EnterState(this);
    }
}