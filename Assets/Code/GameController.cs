using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int maxFPS;
    private Canvas pauseMenu;
    [HideInInspector] public bool pauseMenuActive;
    [HideInInspector] public bool inspectionActive;

    private CrosshairController crosshairController;

    // Start is called before the first frame update
    void Start()
    {
        crosshairController = FindObjectOfType<CrosshairController>();
        pauseMenu = GameObject.Find("Pause Menu").GetComponent<Canvas>();
        pauseMenu.enabled = false;

        Application.targetFrameRate = maxFPS;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!pauseMenuActive)
			{
                ShowCursor();
                pauseMenu.enabled = true;
                pauseMenuActive = true;
            }
            else
			{
                ClosePauseMenu();
            }
		}
    }
    public void ClosePauseMenu()
	{
        HideCursor();
        pauseMenu.enabled = false;
        pauseMenuActive = false;
        if (inspectionActive)
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
    public bool PauseMenuActive()
	{
        return pauseMenuActive;
	}
}
