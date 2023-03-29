using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum State
    {
        FreeLook,
        Inspection,
        PauseMenu
    }

    State currentState;
    State previousState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.FreeLook;
        previousState = currentState;
    }

    // Update is called once per frame
    void Update()
    {

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
