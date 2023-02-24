using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Mouse sensitivity.
    public float sensitivity = 90f;

    //Variables to restrict camera from looking too far down or up.
    //For some reason Unity mouse is inverted??
    private readonly float floorAngleLimit = 15f;
    private readonly float ceilingAngleLimit = -25f;

    //Variables to restrict camera from looking too far left or right.
    private readonly float leftAngleLimit = 35f;
    private readonly float rightAngleLimit = -35f;

    //Variables for horizontal and vertical movement.
    private float horizontalMovement = 0f;
    private float verticalMovement = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Locks cursor into game, and hides default mouse pointer.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Gets variables for changing horizontal and vertical movement
        var horizontal = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
        var vertical = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;

        //Adds new values to movement variables.
        //Vertical movement is inverted for some reason, so negative value is used to un invert! :D
        horizontalMovement += horizontal;
        verticalMovement += -vertical;

        //Restricts camera movement from crossing certain points on the screen.
        horizontalMovement = Mathf.Clamp(horizontalMovement, rightAngleLimit, leftAngleLimit);
        verticalMovement = Mathf.Clamp(verticalMovement, ceilingAngleLimit, floorAngleLimit);

        //Transform camera angle based on mouse movement.
        transform.eulerAngles = new Vector3(verticalMovement, horizontalMovement, 0.0f);
    }
}
