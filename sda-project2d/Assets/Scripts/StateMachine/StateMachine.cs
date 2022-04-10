using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private BaseState currentState;

    private void Start()
    {
        EnterState(new GameState());
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