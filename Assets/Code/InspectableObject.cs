using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(Collider))]
public class InspectableObject : MonoBehaviour
{
    //Interaction button.
    [SerializeField] private KeyCode interactionKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode endInteractionKey = KeyCode.E;

    //Scale of inspected object.
    [SerializeField] private float inspectedItemScaleChange;

    //Speed of object rotation.
    [SerializeField] private float inspectionSpeed;

    private GameController gameController;
    private StateMachine stateMachine;
    private GameObject inspectedObject;
    private CameraController cameraController;
    private AudioController audioController;
    [SerializeField] private string audioClipTypePickup;
    [SerializeField] private string audioClipTypePlace;

    private GameObject postFX;

    private Vector3 startingPosition;
    private Quaternion startingRotation;

    public void Start()
    {
        postFX = GameObject.Find("Post FX");
        audioController = FindObjectOfType<AudioController>();
        gameController = FindObjectOfType<GameController>();
        stateMachine = FindObjectOfType<StateMachine>();
        cameraController = FindObjectOfType<CameraController>();
    }

    public void Update()
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
    public void OnMouseOver()
    {
        if (stateMachine.GetCurrentState() == StateMachine.State.FreeLook)
        {
            if (Input.GetKeyDown(interactionKey))
            {
                SetupInspection();
                audioController.Play(audioClipTypePickup.ToString());
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
        ToggleDepthOfField(true);
        Debug.Log("SetupInspection called.");
        inspectedObject = gameObject;

        gameController.ShowCursor();

        startingPosition = gameObject.transform.position;
        startingRotation = gameObject.transform.rotation;

        gameObject.transform.position = Camera.main.transform.position + new Vector3(0, 0, 1);
        gameObject.transform.localScale -= new Vector3(inspectedItemScaleChange, inspectedItemScaleChange, inspectedItemScaleChange);

        cameraController.ToggleInspectionCamera(true);
        stateMachine.ChangeState(StateMachine.State.Transition);
    }

    private void EndInspection()
    {
        ToggleDepthOfField(false);
        Debug.Log("EndInspection called.");

        gameController.HideCursor();

        inspectedObject.transform.SetPositionAndRotation(startingPosition, startingRotation);
        inspectedObject.transform.localScale += new Vector3(inspectedItemScaleChange, inspectedItemScaleChange, inspectedItemScaleChange);
        inspectedObject = null;
        cameraController.ToggleInspectionCamera(false);
    }
    public void ToggleDepthOfField(bool onOrOff)
    {
        if (postFX != null)
        {
            if (onOrOff)
            {
                postFX.GetComponent<PostProcessVolume>().enabled = true;
            }
            else
            {
                postFX.GetComponent<PostProcessVolume>().enabled = false;
            }
        }
    }
}
