using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private BaseState currentState;

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

    public void OnStartGameButtonPressed()
    {
        EnterState(new GameState());
    }
}