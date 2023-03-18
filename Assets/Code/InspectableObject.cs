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
    [SerializeField] private bool inspectionActive = false;

    private GameObject originalObject;
    private GameObject crosshair;
    private Vector3 startingPosition;
    private Quaternion startingRotation;

    public void Start()
    {
        originalObject = gameObject;
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
                SetUpInspection();
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

            originalObject.transform.Rotate(vertical, -horizontal, 0, Space.World);
        }
    }

    private void SetUpInspection()
    {
        print("SETUP INSPECTION CALLED");

        startingPosition = gameObject.transform.position;
        startingRotation = gameObject.transform.rotation;

        inspectionActive = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        crosshair.SetActive(false);

        originalObject.transform.position = Camera.main.transform.position + new Vector3(0, 0, 1);
        originalObject.transform.localScale += new Vector3(-inspectedItemScaleChange, -inspectedItemScaleChange, -inspectedItemScaleChange);

        Camera.main.transform.rotation = new Quaternion(0, 0, 0, 0);
        CameraController.inspectionActive = true;
    }

    private void EndInspection()
    {
        print("END INSPECTION CALLED");

        inspectionActive = false;
        CameraController.inspectionActive = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        crosshair.SetActive(true);

        originalObject.transform.SetPositionAndRotation(startingPosition, startingRotation);
        originalObject.transform.localScale += new Vector3(inspectedItemScaleChange, inspectedItemScaleChange, inspectedItemScaleChange);
    }
}
