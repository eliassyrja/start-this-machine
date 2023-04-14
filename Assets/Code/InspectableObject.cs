using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InspectableObject : MonoBehaviour
{
    //Interaction button.
    [SerializeField] private KeyCode interactionKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode endInteractionKey = KeyCode.E;

    [SerializeField] private GameObject innerItem;

    //Scale of inspected object.
    [SerializeField] private float inspectedItemScaleChange;

    //Speed of object rotation.
    [SerializeField] private float inspectionSpeed;

    [SerializeField] private float maxZoom = 0.5f;
    [SerializeField] private float zoomSpeed = 0.5f;
    [SerializeField] private string audioClipTypePickup;
    [SerializeField] private string audioClipTypePlace;

    private Transform cameraContainer;
    private InventoryController inventoryController;
    private StateMachine stateMachine;
    private GameObject inspectedObject;
    private CameraController cameraController;
    private AudioController audioController;
    private GameController gameController;

    private Vector3 startingPosition;
    private Quaternion startingRotation;

    private void Start()
    {
        cameraContainer = GameObject.Find("CameraContainer").transform;
        gameController = FindAnyObjectByType<GameController>();
        inventoryController = FindObjectOfType<InventoryController>();
        audioController = FindObjectOfType<AudioController>();
        stateMachine = FindObjectOfType<StateMachine>();
        cameraController = FindObjectOfType<CameraController>();
    }

    private void Update()
    {
        if (stateMachine.GetCurrentState() == StateMachine.State.Inspection)
        {
            if (Input.GetKeyDown(endInteractionKey) && gameObject == inspectedObject)
            {
                EndInspection();
                audioController.Play(audioClipTypePlace.ToString());
            }
        }
    }

    private void OnMouseOver()
    {
        if (stateMachine.GetCurrentState() == StateMachine.State.FreeLook)
        {
            if (Input.GetKeyDown(interactionKey))
            {
                SetupInspection();
                audioController.Play(audioClipTypePickup.ToString());
            }
        }
        if(stateMachine.GetCurrentState() == StateMachine.State.Inspection && Input.GetKeyDown(KeyCode.C))
		{
			if (gameObject.GetComponent<InventoryItem>())
			{
                inventoryController.AddItem(gameObject.GetComponent<InventoryItem>());
                cameraController.ToggleInspectionCamera(false);
                Destroy(gameObject);
            }
            
		}
    }

    private void OnMouseDrag()
    {
        if (stateMachine.GetCurrentState() == StateMachine.State.Inspection && inspectedObject == gameObject)
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
        gameController.SetCurrentInspectableObject(gameObject.GetComponent<InspectableObject>());
        Debug.Log("SetupInspection called.");
        inspectedObject = gameObject;

        startingPosition = gameObject.transform.position;
        startingRotation = gameObject.transform.rotation;

        gameObject.transform.position = cameraContainer.position + new Vector3(0, 0, 1);
        gameObject.transform.localScale -= new Vector3(inspectedItemScaleChange, inspectedItemScaleChange, inspectedItemScaleChange);

        cameraController.ToggleInspectionCamera(true);
    }

    private void EndInspection()
    {
        gameController.SetCurrentInspectableObject(null);
        Debug.Log("EndInspection called.");

        if (innerItem != null)
        {
            innerItem.GetComponent<InnerInspectableObject>().ResetTransform();
        }
        inspectedObject.transform.SetPositionAndRotation(startingPosition, startingRotation);
        inspectedObject.transform.localScale += new Vector3(inspectedItemScaleChange, inspectedItemScaleChange, inspectedItemScaleChange);
        inspectedObject = null;
        cameraController.ToggleInspectionCamera(false);
    }

    public float GetMaxZoom()
	{
        return maxZoom;
	}

    public float GetZoomSpeed()
    {
        return zoomSpeed;
    }
}
