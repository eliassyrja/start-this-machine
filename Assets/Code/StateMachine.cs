using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StateMachine : MonoBehaviour
{
    private GameController gameController;
    [SerializeField] private GameObject stateText;
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

	public void ChangeState(State newState)
    {
        previousState = currentState;
        currentState = newState;
        stateText.GetComponent<TextMeshProUGUI>().text = currentState.ToString();

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
