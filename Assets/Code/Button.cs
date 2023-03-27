using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private bool buttonState;
    public Light lightIndicator;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize button state to be false = off
        buttonState = false;
    }

	private void OnMouseOver()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
            UseButton();
		}
	}

    private void UseButton()
	{
        Debug.Log("UseButton called");
        if (buttonState)
		{
            Debug.Log("Button off");
            lightIndicator.color = Color.red;
            //Local rotations of the switch, current values eyeballed from editor
            transform.localRotation = Quaternion.Euler(22,0,0);
            buttonState = false;
        }
		else
		{
            Debug.Log("Button on");
            lightIndicator.color = Color.green;
            //Local rotations of the switch, current values eyeballed from editor
            transform.localRotation = Quaternion.Euler(-22,0,0);
            buttonState = true;
        }
        
	}
}
