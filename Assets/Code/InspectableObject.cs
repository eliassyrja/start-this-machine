using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InspectableObject : MonoBehaviour
{
	//Interaction button.
	[SerializeField] private KeyCode interactionKey = KeyCode.Mouse0;

	//Scale of inspected object.
	[SerializeField] private float inspectedItemScaleChange;

	//Speed of object rotation.
	[SerializeField] private float inspectionSpeed;

	private GameController gameController;
	private StateMachine stateMachine;
	private GameObject inspectedObject;
	private CameraController cameraController;

	private Vector3 startingPosition;
	private Quaternion startingRotation;

	public void Start()
	{
		gameController = FindObjectOfType<GameController>();
		stateMachine = FindObjectOfType<StateMachine>();
		cameraController = FindObjectOfType<CameraController>();
	}

	public void Update()
	{
		if (stateMachine.GetCurrentState() == StateMachine.State.Inspection)
		{
			if (Input.GetKeyDown(KeyCode.E) && gameObject == inspectedObject)
			{
				EndInspection();
			}
		}
	}
	public void OnMouseOver()
	{
		if (stateMachine.GetCurrentState() == StateMachine.State.FreeLook)
		{
			if (Input.GetKeyDown(interactionKey))
			{
				SetupInspection();
			}
		}
	}

	public void OnMouseDrag()
	{
		if (stateMachine.GetCurrentState() == StateMachine.State.Inspection)
		{
			InspectItem();
		}
	}

	private void InspectItem()
	{
		if (Input.GetKey(interactionKey))
		{
			var horizontal = Input.GetAxis("Mouse X") * inspectionSpeed;
			var vertical = Input.GetAxis("Mouse Y") * inspectionSpeed;

			gameObject.transform.Rotate(vertical, -horizontal, 0, Space.World);
		}
	}

	private void SetupInspection()
	{
		Debug.Log("SetupInspection called.");
		stateMachine.ChangeState(StateMachine.State.Inspection);

		inspectedObject = gameObject;

		gameController.ShowCursor();
		startingPosition = gameObject.transform.position;
		startingRotation = gameObject.transform.rotation;

		gameObject.transform.position = Camera.main.transform.position + new Vector3(0, 0, 1);
		gameObject.transform.localScale -= new Vector3(inspectedItemScaleChange, inspectedItemScaleChange, inspectedItemScaleChange);

		cameraController.HandleInspectionCamera(true);
	}

	private void EndInspection()
	{
		Debug.Log("EndInspection called.");

		gameController.HideCursor();
		stateMachine.ChangeState(StateMachine.State.FreeLook);

		inspectedObject.transform.SetPositionAndRotation(startingPosition, startingRotation);
		inspectedObject.transform.localScale += new Vector3(inspectedItemScaleChange, inspectedItemScaleChange, inspectedItemScaleChange);
		inspectedObject = null;
		cameraController.HandleInspectionCamera(false);
	}
}
