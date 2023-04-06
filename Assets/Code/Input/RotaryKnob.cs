using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class RotaryKnob : MonoBehaviour
{
	[SerializeField] public float buttonValue;
	private float valueRangeMin = 0f;
	private float valueRangeMax = 1f;
	[SerializeField] private float rotationSpeed;

	[SerializeField] private float rotaryValueMin = 0f;
	[SerializeField] private float rotaryValueMax = 180f;

	private StateMachine stateMachine;

	// Start is called before the first frame update
	void Start()
	{
		stateMachine = FindObjectOfType<StateMachine>();
		//Initialize button state to be false = off
		buttonValue = valueRangeMin;
	}

	private void OnMouseDrag()
	{
		if (stateMachine.GetCurrentState() == StateMachine.State.FreeLook)
		{
		}
	}
}
