using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ToggleSwitch : MonoBehaviour
{
	public bool switchState;
	private bool clickable;
	public bool isPowerSwitch;
	[SerializeField] private float transitionTime;
	public Light lightIndicator;
	private AudioController audioController;

	private StateMachine stateMachine;

	//Event for calling different functions on different buttons
	[SerializeField] private UnityEvent switchOnEvent;
	[SerializeField] private UnityEvent switchOffEvent;

	// Start is called before the first frame update
	void Start()
	{
		audioController = FindObjectOfType<AudioController>();
		stateMachine = FindObjectOfType<StateMachine>();
		//Initialize button state to be false = off
		switchState = false;
		clickable = true;
		lightIndicator.enabled = false;
	}

	private void OnMouseOver()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0) && clickable && stateMachine.GetCurrentState() == StateMachine.State.FreeLook)
		{
			UseSwitch();
		}
	}

	private void UseSwitch()
	{
		audioController.Play("FlickSwitch");
		Debug.Log("UseSwitch called");
		if (switchState)
		{
			StartCoroutine(ChangeRotationSmoothly(Quaternion.Euler(-22, 0, 0), Quaternion.Euler(22, 0, 0), transitionTime));
			switchState = false;
			Debug.Log("ToggleSwitch off");

			if (GameController.powerOn || isPowerSwitch)
			{
				lightIndicator.enabled = true;
				SwitchStateEvent(false);
			}
		}
		else
		{
			StartCoroutine(ChangeRotationSmoothly(Quaternion.Euler(22, 0, 0), Quaternion.Euler(-22, 0, 0), transitionTime));
			switchState = true;
			Debug.Log("ToggleSwitch on");

			if (GameController.powerOn || isPowerSwitch)
			{
				lightIndicator.enabled = true;
				SwitchStateEvent(true);
			}
		}
	}
	public void SwitchStateEvent(bool state)
	{
		if (state)
		{
			lightIndicator.color = Color.green;
			switchOnEvent.Invoke();
		}
		else
		{
			lightIndicator.color = Color.red;
			switchOffEvent.Invoke();
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
