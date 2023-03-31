using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int maxFPS;
    private Canvas pauseMenu;

    private CrosshairController crosshairController;
    private StateMachine stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        crosshairController = FindObjectOfType<CrosshairController>();
        stateMachine = FindObjectOfType<StateMachine>();
        pauseMenu = GameObject.Find("Pause Menu").GetComponent<Canvas>();
        pauseMenu.enabled = false;

        Application.targetFrameRate = maxFPS;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (stateMachine.GetCurrentState() != StateMachine.State.PauseMenu)
            {
                ShowPauseMenu();
            }
            else
            {
                ClosePauseMenu();
            }
        }
    }

    public void ShowPauseMenu()
    {
        stateMachine.ChangeState(StateMachine.State.PauseMenu);
        ShowCursor();
        pauseMenu.enabled = true;
    }

    public void ClosePauseMenu()
    {
        if (stateMachine.GetPreviousState() == StateMachine.State.Inspection)
        {
            stateMachine.ChangeState(StateMachine.State.Inspection);
        }
        else
        {
            stateMachine.ChangeState(StateMachine.State.FreeLook);
        }

        HideCursor();
        pauseMenu.enabled = false;
        if (stateMachine.GetCurrentState() == StateMachine.State.Inspection)
        {
            ShowCursor();
        }
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        crosshairController.HideCrosshair();
    }
    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        crosshairController.ShowCrosshair();
    }

}
