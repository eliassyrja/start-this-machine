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

    private bool inspectionActive;
    private Vector3 startingPosition;
    private Quaternion startingRotation;

    public void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void Update()
    {
        if (inspectionActive)
        {
            if (Input.GetKeyDown(KeyCode.E) && gameController.pauseMenuActive == false)
            {
                EndInspection();
            }
        }
    }
    public void OnMouseOver()
    {
        if (Input.GetKeyDown(interactionKey) && gameController.pauseMenuActive == false)
        {
            if (!inspectionActive)
            {
                SetupInspection();
            }
        }
    }

    public void OnMouseDrag()
    {
        if (inspectionActive && gameController.pauseMenuActive == false)
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
        gameController.SetInspectionAcive(true);
        inspectionActive = gameController.IsInspectionActive();

        gameController.ShowCursor();

        gameObject.transform.position = Camera.main.transform.position + new Vector3(0, 0, 1);
        gameObject.transform.localScale -= new Vector3(inspectedItemScaleChange, inspectedItemScaleChange, inspectedItemScaleChange);

        Camera.main.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void EndInspection()
    {
        Debug.Log("EndInspection called.");

        gameController.SetInspectionAcive(false);
        inspectionActive = gameController.IsInspectionActive();
        gameController.HideCursor();

        gameObject.transform.SetPositionAndRotation(startingPosition, startingRotation);
        gameObject.transform.localScale += new Vector3(inspectedItemScaleChange, inspectedItemScaleChange, inspectedItemScaleChange);
    }
}
