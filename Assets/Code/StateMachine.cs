using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StateMachine : MonoBehaviour
{
    private GameController gameController;
    private DebugController debug;
    private ZoomController zoomController;

    public enum State
    {
        FreeLook,
        Inspection,
        PauseMenu,
        Transition
    }

    private State currentState;
    private State previousState;

    // Start is called before the first frame update
    private void Start()
    {
        zoomController = Camera.main.GetComponent<ZoomController>();
        gameController = FindObjectOfType<GameController>();
        debug = FindObjectOfType<DebugController>();
        currentState = State.FreeLook;
        previousState = currentState;
        debug.UpdateStateText(currentState.ToString(), previousState.ToString());
    }

    public void ChangeState(State newState)
    {
        previousState = currentState;
        currentState = newState;
        debug.UpdateStateText(currentState.ToString(), previousState.ToString());

        if (currentState != State.PauseMenu)
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
        if (previousState == State.Inspection)
        {
            gameController.ToggleDepthOfField(false);
            zoomController.SetDefaultZoomParameters();
        }

        switch (newState)
        {
            case State.FreeLook:
                gameController.HideCursor();
                break;
            case State.Inspection:
                gameController.ToggleDepthOfField(true);
                gameController.ShowCursor();
                zoomController.ChangeZoomParameters(gameController.GetCurrentInspectableObject());
                break;
            case State.PauseMenu:
                gameController.ShowCursor();
                AudioListener.pause = true;
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
