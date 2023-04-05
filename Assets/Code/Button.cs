using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
	public bool buttonState;
	private bool clickable;
	public bool isPowerSwitch;
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
		lightIndicator.enabled = false;
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
			StartCoroutine(ChangeRotationSmoothly(Quaternion.Euler(-22, 0, 0), Quaternion.Euler(22, 0, 0), transitionTime));
			buttonState = false;
			Debug.Log("Button off");

			if (GameController.powerOn || isPowerSwitch)
			{
				lightIndicator.enabled = true;
				ButtonStateEvent(false);
			}
		}
		else
		{
			StartCoroutine(ChangeRotationSmoothly(Quaternion.Euler(22, 0, 0), Quaternion.Euler(-22, 0, 0), transitionTime));
			buttonState = true;
			Debug.Log("Button on");

			if (GameController.powerOn || isPowerSwitch)
			{
				lightIndicator.enabled = true;
				ButtonStateEvent(true);
			}
		}
	}
	public void ButtonStateEvent(bool state)
	{
		if (state)
		{
			lightIndicator.color = Color.green;
			buttonOnEvent.Invoke();
		}
		else
		{
			lightIndicator.color = Color.red;
			buttonOffEvent.Invoke();
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
