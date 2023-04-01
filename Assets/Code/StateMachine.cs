using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private GameController gameController;
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
        currentState = State.FreeLook;
        previousState = currentState;
    }
	private void Update()
	{
        Debug.Log("State is: " + currentState);
	}

	public void ChangeState(State newState)
    {
        previousState = currentState;
        currentState = newState;

        if (previousState == State.PauseMenu)
		{
            Time.timeScale = 1;
		}

        switch (newState)
        {
            case State.FreeLook:
                gameController.HideCursor();
                break;
            case State.Inspection:
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
