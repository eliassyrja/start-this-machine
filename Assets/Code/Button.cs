using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
	public bool buttonState;
	private bool clickable;
	[SerializeField] private float transitionTime;
	public Light lightIndicator;
	private AudioController audioController;

	private StateMachine stateMachine;

	//Event for calling different functions on different buttons
	[SerializeField] private UnityEvent buttonOnEvent;
	[SerializeField] private UnityEvent buttonOffEvent;

	// Start is called before the first frame update
	void Start()
	{
		audioController = FindObjectOfType<AudioController>();
		stateMachine = FindObjectOfType<StateMachine>();
		//Initialize button state to be false = off
		buttonState = false;
		clickable = true;
	}

	private void OnMouseOver()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0) && clickable && stateMachine.GetCurrentState() == StateMachine.State.FreeLook)
		{
			UseButton();
		}
	}

	private void UseButton()
	{
		audioController.Play("FlickSwitch");
		Debug.Log("UseButton called");
		if (buttonState)
		{
			buttonState = false;
			buttonOffEvent.Invoke();
			
			Debug.Log("Button off");
			lightIndicator.color = Color.red;
			//Local rotations of the switch, current values eyeballed from editor
			//transform.localRotation = Quaternion.Euler(22, 0, 0);
			StartCoroutine(ChangeRotationSmoothly(Quaternion.Euler(-22, 0, 0), Quaternion.Euler(22, 0, 0), transitionTime));
			
		}
		else
		{
			buttonState = true;
			buttonOnEvent.Invoke();
			

			Debug.Log("Button on");
			lightIndicator.color = Color.green;
			//Local rotations of the switch, current values eyeballed from editor
			//transform.localRotation = Quaternion.Euler(-22, 0, 0);
			StartCoroutine(ChangeRotationSmoothly(Quaternion.Euler(22, 0, 0), Quaternion.Euler(-22, 0, 0), transitionTime));
			
		}
	}

	// Coroutine to change rotation smoothly over time. It seems like Lerp can only be properly used inside of Update() -method.
	IEnumerator ChangeRotationSmoothly(Quaternion startPosition, Quaternion endPosition, float time)
	{
		float startTime = Time.time;
		clickable = false;
		while (Time.time < startTime + time)
		{
			transform.localRotation = Quaternion.Lerp(startPosition, endPosition, (Time.time - startTime) / time);
			yield return null;
		}
		transform.localRotation = endPosition;
		clickable = true;
	}
}
