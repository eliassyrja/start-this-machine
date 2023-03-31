using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
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

        switch (newState)
        {
            case State.FreeLook:
                break;
            case State.Inspection:
                break;
            case State.PauseMenu:
                break;
            case State.Transition:
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
