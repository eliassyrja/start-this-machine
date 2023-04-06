using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class RotaryKnob : MonoBehaviour
{
	[SerializeField] public float buttonValue;
	[SerializeField] private float valueRangeMin = 0f;
	[SerializeField] private float valueRangeMax = 5f;

	[SerializeField] private float rotationSpeed;
	[SerializeField] private float rotaryValueMin = 0f;
	[SerializeField] private float rotaryValueMax = 300f;

	[SerializeField] private float horizontal;

	private StateMachine stateMachine;

	// Start is called before the first frame update
	void Start()
	{
		stateMachine = FindObjectOfType<StateMachine>();
		buttonValue = valueRangeMin;
	}

	private void OnMouseDrag()
	{
		if (stateMachine.GetCurrentState() == StateMachine.State.FreeLook)
		{
			RotateKnob();
		}
	}
	private void RotateKnob()
	{
		horizontal += Input.GetAxis("Mouse X") * rotationSpeed;
		horizontal = Mathf.Clamp(horizontal,  rotaryValueMin, rotaryValueMax);
		Vector3 oldRotation;
		oldRotation = transform.localRotation.eulerAngles;

		oldRotation.z = horizontal;
		transform.rotation = Quaternion.Euler(oldRotation);

		buttonValue = Mathf.Lerp(valueRangeMin, valueRangeMax, horizontal/rotaryValueMax);
	}
}
