using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;
using DepthOfField = UnityEngine.Rendering.HighDefinition.DepthOfField;

public class GameController : MonoBehaviour
{
    [SerializeField] private bool lightsOnAtStart;
    public int maxFPS;
    private InspectableObject currentInspectableObject;


    private Canvas pauseMenu;
    private Canvas settingsMenu;

    private CrosshairController crosshairController;
    private StateMachine stateMachine;

    public static bool powerOn;
    private ToggleSwitch[] buttons;
    private Light[] lights;

    // Start is called before the first frame update
    void Start()
    {
        lights = FindObjectsOfType<Light>();
        buttons = FindObjectsOfType<ToggleSwitch>();
		if (lightsOnAtStart)
		{
            powerOn = true;
		}
		else
		{
            powerOn = false;
			foreach (Light light in lights)
			{
                light.enabled = false;
			}
        }
        
        crosshairController = FindObjectOfType<CrosshairController>();
        stateMachine = FindObjectOfType<StateMachine>();

        pauseMenu = GameObject.Find("Pause Menu").GetComponent<Canvas>();
        pauseMenu.enabled = false;

        settingsMenu = GameObject.Find("Settings Menu").GetComponent<Canvas>();
        settingsMenu.enabled = false;

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
            else if (settingsMenu.enabled == false)
            {
                ClosePauseMenu();
            }
            else
            {
                CloseSettingsMenu();
            }
        }
    }

    public void SetCurrentInspectableObject(InspectableObject _currentInspectableObject)
	{
        currentInspectableObject = _currentInspectableObject;
	}
    public InspectableObject GetCurrentInspectableObject()
    {
        return currentInspectableObject;
    }

    public void ShowPauseMenu()
    {
        if (stateMachine.GetCurrentState() != StateMachine.State.PauseMenu)
        {
            stateMachine.ChangeState(StateMachine.State.PauseMenu);
        }
        pauseMenu.enabled = true;
    }

    public void ClosePauseMenu()
    {
        stateMachine.ChangeState(stateMachine.GetPreviousState());
        pauseMenu.enabled = false;
    }

    public void ShowSettingsMenu()
    {
        pauseMenu.enabled = false;
        settingsMenu.enabled = true;
    }

    public void CloseSettingsMenu()
    {
        settingsMenu.enabled = false;
        ShowPauseMenu();
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

    public void QuitGame()
    {
        //TODO: remove this comment when building
        //Application.Quit();

        //Use this for editor testing
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ToggleDepthOfField(bool isEnabled)
    {
        if (isEnabled)
        {
        }
        else
        {
        }
    }

}
