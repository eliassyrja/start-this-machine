using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
	//Keycodes and camera to zoom functionality.
	[SerializeField] KeyCode zoomKey = KeyCode.Mouse1;
	[SerializeField] Camera cam;

	//Variables needed for zooming to mouse pointer.
	[SerializeField] private float startingZoom;
	[SerializeField] private float maxZoom;
	[SerializeField] private float zoomSpeed;

	//Variable used to store camera FOV while zooming in and out.
	private float currentZoom;
	private float defaultMaxZoom;
	private float defaultZoomSpeed;

	private StateMachine stateMachine;

	// Start is called before the first frame update
	void Start()
	{
		defaultMaxZoom = maxZoom;
		defaultZoomSpeed = zoomSpeed;
		//Set Camera FOV to starting FOV, this can be changed in Editor.
		stateMachine = FindObjectOfType<StateMachine>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(zoomKey) && stateMachine.GetCurrentState() != StateMachine.State.PauseMenu && stateMachine.GetCurrentState() != StateMachine.State.Transition)
		{
			HandleCameraZoomIn();
		}
		else if (stateMachine.GetCurrentState() != StateMachine.State.PauseMenu)
		{
			HandleCameraZoomOut();
		}

	}
	private void HandleCameraZoomIn()
	{
		currentZoom += zoomSpeed * Time.deltaTime;
		if (currentZoom < maxZoom)
		{
			cam.transform.localPosition = new Vector3(0, 0, currentZoom);
		}
		else
		{
			currentZoom = maxZoom;
			cam.transform.localPosition = new Vector3(0, 0, currentZoom);
		}
	}

	private void HandleCameraZoomOut()
	{
		currentZoom -= zoomSpeed * Time.deltaTime;
		if (currentZoom > startingZoom)
		{
			cam.transform.localPosition = new Vector3(0, 0, currentZoom);
		}
		else
		{
			currentZoom = startingZoom;
			cam.transform.localPosition = new Vector3(0, 0, currentZoom);
		}
	}

	public void ChangeZoomParameters(InspectableObject inspectableObject)
	{
		if (inspectableObject != null)
		{
			maxZoom = inspectableObject.GetMaxZoom();
			zoomSpeed = inspectableObject.GetZoomSpeed();
		}
	}

	public void SetDefaultZoomParameters()
	{
		maxZoom = defaultMaxZoom;
		zoomSpeed = defaultZoomSpeed;
	}


}
