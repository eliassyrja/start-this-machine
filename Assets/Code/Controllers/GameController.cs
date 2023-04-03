using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;
using UnityEngine;
using DepthOfField = UnityEngine.Rendering.HighDefinition.DepthOfField;

public class GameController : MonoBehaviour
{
    public int maxFPS;
    private Canvas pauseMenu;
    private Canvas settingsMenu;
    [SerializeField] private GameObject pauseMenuBlur;

    private VolumeProfile profile;
    private Volume volume;
    private DepthOfField depthOfField;

    private CrosshairController crosshairController;
    private StateMachine stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuBlur.SetActive(false);

        volume = GameObject.Find("HDRP Post FX").GetComponent<Volume>();
        profile = volume.sharedProfile;
        profile.TryGet(out depthOfField);
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

    public void ShowPauseMenu()
    {
        if (stateMachine.GetCurrentState() != StateMachine.State.PauseMenu)
        {
            stateMachine.ChangeState(StateMachine.State.PauseMenu);
        }
        pauseMenu.enabled = true;
        pauseMenuBlur.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        stateMachine.ChangeState(stateMachine.GetPreviousState());
        pauseMenu.enabled = false;
        pauseMenuBlur.SetActive(false);
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
            depthOfField.active = true;
        }
        else
        {
            depthOfField.active = false;
        }

    }
}
