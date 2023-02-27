using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Mouse sensitivity.
    public float sensitivity = 90f;

    //Variables to restrict camera from looking too far down or up.
    //For some reason Unity mouse is inverted??
    [SerializeField] private float floorAngleLimit = 15.0f;
    [SerializeField] private float ceilingAngleLimit = -25.0f;

    //Variables to restrict camera from looking too far left or right.
    [SerializeField] private float leftAngleLimit = 35.0f;
    [SerializeField] private float rightAngleLimit = -35.0f;

    //Variables for horizontal and vertical movement.
    private float horizontalMovement;
    private float verticalMovement;

    //Variables needed for zooming to mouse pointer.
    private float minFov = 15.0f;
    private float maxFov = 90.0f;
    private float zoomSpeed = 50.0f;
    private float fov;


    //Keycodes to move and zoom functionality.
    [SerializeField] KeyCode zoomKey = KeyCode.Mouse1;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleCameraMovement();
        Zoom();
    }

    private void HandleCameraMovement()
    {
        //Gets variables for changing horizontal and vertical movement
        var horizontal = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        var vertical = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

        //Adds new values to movement variables.
        //Vertical movement is inverted for some reason, so negative value is used to un invert! :D
        horizontalMovement += horizontal;
        verticalMovement -= vertical;

        //Restricts camera movement from crossing certain points on the screen.
        horizontalMovement = Mathf.Clamp(horizontalMovement, rightAngleLimit, leftAngleLimit);
        verticalMovement = Mathf.Clamp(verticalMovement, ceilingAngleLimit, floorAngleLimit);

        //Transform camera angle based on mouse movement.
        transform.eulerAngles = new Vector3(verticalMovement, horizontalMovement, 0.0f);

    }

    private void Zoom()
    {
        fov = Camera.main.fieldOfView;
        fov += (Input.GetAxis("Mouse ScrollWheel") * zoomSpeed) * -1;
        fov = Mathf.Clamp(fov, minFov, maxFov);
    }
}
