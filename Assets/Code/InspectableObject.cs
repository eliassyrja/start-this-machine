using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class InspectableObject : MonoBehaviour
{
    //Interaction button.
    [SerializeField] private KeyCode interactionKey = KeyCode.Mouse0;

    //Scale of inspected object.
    [SerializeField] private float inspectedItemScaleChange;

    //Speed of object rotation.
    [SerializeField] private float inspectionSpeed;

    private bool inspectionActive;
    private GameObject crosshair;
    private Vector3 startingPosition;
    private Quaternion startingRotation;

    public void Start()
    {
        crosshair = GameObject.Find("Crosshair");
    }

    public void Update()
    {
        if (inspectionActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                EndInspection();
            }
        }
    }
    public void OnMouseOver()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            print("Interaction attempted");
            if (!inspectionActive)
            {
                SetupInspection();
            }
        }
    }

    public void OnMouseDrag()
    {
        if (inspectionActive)
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

        startingPosition = gameObject.transform.position;
        startingRotation = gameObject.transform.rotation;
        inspectionActive = true;
        CameraController.inspectionActive = true;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        crosshair.SetActive(false);

        gameObject.transform.position = Camera.main.transform.position + new Vector3(0, 0, 1);
        gameObject.transform.localScale -= new Vector3(inspectedItemScaleChange, inspectedItemScaleChange, inspectedItemScaleChange);

        Camera.main.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void EndInspection()
    {
        Debug.Log("EndInspection called.");

        inspectionActive = false;
        CameraController.inspectionActive = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        crosshair.SetActive(true);

        gameObject.transform.SetPositionAndRotation(startingPosition, startingRotation);
        gameObject.transform.localScale += new Vector3(inspectedItemScaleChange, inspectedItemScaleChange, inspectedItemScaleChange);
    }
}
