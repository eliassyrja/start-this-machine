using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InspectableObject : MonoBehaviour
{
    //Interaction button.
    [SerializeField] private KeyCode interactionKey = KeyCode.Mouse0;

    //Cameras for easier handling in Unity Editor.
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera inspectionCamera;

    //Scale of inspected object.
    [SerializeField] private float inspectedItemScaleChange;

    //Speed of object rotation.
    [SerializeField] private float inspectionSpeed;
    [SerializeField] private bool inspectionActive = false;
    
    private GameObject originalObject;
    private Vector3 startingPosition;
    private Quaternion startingRotation;

    public void Start()
    {
        originalObject = gameObject;
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

        originalObject.transform.position = Camera.main.transform.position + new Vector3(0, 0, 1);
        originalObject.transform.localScale += new Vector3(-inspectedItemScaleChange, -inspectedItemScaleChange, -inspectedItemScaleChange);

        mainCamera.enabled = false;
        inspectionCamera.enabled = true;
    }

    private void EndInspection()
    {
        print("END INSPECTION CALLED");

        inspectionActive = false;
        mainCamera.enabled = true;
        inspectionCamera.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        originalObject.transform.rotation = startingRotation;
        originalObject.transform.position = startingPosition;
        originalObject.transform.localScale += new Vector3(inspectedItemScaleChange, inspectedItemScaleChange, inspectedItemScaleChange);
    }
}
