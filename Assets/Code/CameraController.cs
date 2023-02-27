using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Mouse sensitivity.
    public float sensitivity = 20f;

    //Variables to restrict camera from looking too far down or up.
    //For some reason Unity mouse is inverted??
    [SerializeField] private float floorAngleLimit = 30f;
    [SerializeField] private float ceilingAngleLimit = -50f;

    //Variables to restrict camera from looking too far left or right.
    [SerializeField] private float leftAngleLimit = 70f;
    [SerializeField] private float rightAngleLimit = -70f;

    //Variables for horizontal and vertical movement.
    private float horizontalMovement;
    private float verticalMovement;

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
    }

    private void HandleCameraMovement()
    {
        //Gets variables for changing horizontal and vertical movement
        var horizontal = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity * Camera.main.fieldOfView;
        var vertical = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity * Camera.main.fieldOfView;

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
}
