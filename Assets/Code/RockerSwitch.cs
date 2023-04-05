using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RockerSwitch : MonoBehaviour
{
	[SerializeField] public bool buttonState;
	private bool clickable;
	[SerializeField] private float transitionTime;
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
			StartCoroutine(ChangeRotationSmoothly(Quaternion.Euler(24, 0, 0), Quaternion.Euler(-7, 0, 0), transitionTime));
			buttonState = false;
			Debug.Log("Button off");

			if (GameController.powerOn)
			{
				ButtonStateEvent(false);
			}
		}
		else
		{
			StartCoroutine(ChangeRotationSmoothly(Quaternion.Euler(-7, 0, 0), Quaternion.Euler(24, 0, 0), transitionTime));
			buttonState = true;
			Debug.Log("Button on");

			if (GameController.powerOn)
			{
				ButtonStateEvent(true);
			}
		}
	}
	public void ButtonStateEvent(bool state)
	{
		if (state)
		{
			buttonOnEvent.Invoke();
		}
		else
		{
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
