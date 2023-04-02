using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StateMachine : MonoBehaviour
{
    private GameController gameController;
    private DebugController debug;

    public enum State
    {
        FreeLook,
        Inspection,
        PauseMenu,
        Transition
    }

    State currentState;
    State previousState;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindAnyObjectByType<GameController>();
        debug = FindAnyObjectByType<DebugController>();
        currentState = State.FreeLook;
        previousState = currentState;
        debug.UpdateStateText(currentState.ToString());
    }

    public void ChangeState(State newState)
    {

        previousState = currentState;
        currentState = newState;
        debug.UpdateStateText(currentState.ToString());

        if (currentState != State.PauseMenu)
        {
            Time.timeScale = 1;
        }
        if (previousState == State.Inspection)
        {
            gameController.ToggleDepthOfField(false);
        }

        switch (newState)
        {
            case State.FreeLook:
                gameController.HideCursor();
                break;
            case State.Inspection:
                gameController.ToggleDepthOfField(true);
                gameController.ShowCursor();
                break;
            case State.PauseMenu:
                gameController.ShowCursor();
                Time.timeScale = 0;
                break;
            case State.Transition:
                gameController.HideCursor();
                break;
        }
    }

    public State GetCurrentState()
    {
        return currentState;
    }

    public State GetPreviousState()
    {
        return previousState;
    }
}
