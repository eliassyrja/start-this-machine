using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldButton : MonoBehaviour
{
    private CameraController cameraController;
    private InventoryController inventoryController;
    public Image fillImage;
    public float fillSpeed = 0.5f;
    public string triggerKey;

    private bool isPressed = false;

	private void Start()
	{
        cameraController = FindObjectOfType<CameraController>();
        inventoryController = FindObjectOfType<InventoryController>();
	}
	private void Update()
    {
        // Check for the specified key being pressed and released
        if (Input.GetKeyDown(triggerKey))
        {
            isPressed = true;
        }
        else if (Input.GetKeyUp(triggerKey))
        {
            isPressed = false;
            fillImage.fillAmount = 0;
        }

        if (isPressed)
        {
            fillImage.fillAmount += fillSpeed * Time.deltaTime;

            if (fillImage.fillAmount >= 1)
            {
                ButtonPressed();
            }
        }
    }

    private void ButtonPressed()
    {
        // Perform the desired action when the button is fully filled
        Debug.Log("Button Pressed");
        Instantiate(inventoryController.itemSlots[int.Parse(triggerKey) - 1].Item.prefab, Camera.main.transform.position + new Vector3(0, 0, 1), Quaternion.Euler(0,0,0));

        // Reset the fill amount and the pressed state
        fillImage.fillAmount = 0;
        isPressed = false;
        cameraController.ToggleInspectionCamera(true);
    }
}
