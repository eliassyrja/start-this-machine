using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InspectableObject : MonoBehaviour
{
    //Interaction button.
    [SerializeField] private KeyCode interactionKey = KeyCode.Mouse0;

    //GameObject that this script is assigned to, clone of the object will be inspected.
    [SerializeField] private GameObject inspectedObject;

    //Cameras for easier handling in Unity Editor.
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera inspectionCamera;

    //Scale of inspected object.
    [SerializeField] private float inspectedItemScaleChange;

    //Speed of object rotation.
    [SerializeField] private float inspectionSpeed;
    [SerializeField] private bool inspectionActive = false;
    
    private GameObject originalObject;

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
            if (Input.GetKeyDown(KeyCode.E))
            {
                EndInspection();
            }
        }
    }

    private void InspectItem()
    {
        if (Input.GetKey(interactionKey))
        {
            var horizontal = Input.GetAxis("Mouse X") * inspectionSpeed;
            var vertical = Input.GetAxis("Mouse Y") * inspectionSpeed;

            inspectedObject.transform.Rotate(vertical, -horizontal, 0, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            EndInspection();
        }
    }

    private void SetUpInspection()
    {
        print("SETUP INSPECTION CALLED");
        inspectionActive = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        originalObject = gameObject;

        inspectedObject = Instantiate(originalObject, Camera.main.transform.position, new Quaternion(0, 0, 0, 0));
        inspectedObject.SetActive(true);
        inspectedObject.transform.position += new Vector3(0, -0.25f, 1);
        inspectedObject.transform.localScale += new Vector3(-inspectedItemScaleChange, -inspectedItemScaleChange, -inspectedItemScaleChange);

        originalObject.SetActive(false);

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

        originalObject.SetActive(true);

        Destroy(inspectedObject);
    }
}
