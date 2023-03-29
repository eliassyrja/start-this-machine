using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
    //Keycodes and camera to zoom functionality.
    [SerializeField] KeyCode zoomKey = KeyCode.Mouse1;
    [SerializeField] Camera cam;

    //Variables needed for zooming to mouse pointer.
    [SerializeField] private float startingFov;
    [SerializeField] private float zoomInFov;
    [SerializeField] private float zoomSpeed;

    //Variable used to store camera FOV while zooming in and out.
    private float currentFov;

    private StateMachine stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        //Set Camera FOV to starting FOV, this can be changed in Editor.
        cam.fieldOfView = startingFov;
        stateMachine = FindObjectOfType<StateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(zoomKey) && stateMachine.GetCurrentState() != StateMachine.State.PauseMenu)
        {
            HandleCameraZoomIn();
        }
        else if (stateMachine.GetCurrentState() != StateMachine.State.PauseMenu)
        {
            HandleCameraZoomOut();
        }

    }

    private void HandleCameraZoomIn()
    {
        currentFov = cam.fieldOfView;
        if (currentFov > zoomInFov)
        {
            cam.fieldOfView += (-zoomSpeed * Time.deltaTime);
        }
    }

    private void HandleCameraZoomOut()
    {
        currentFov = cam.fieldOfView;
        if (currentFov < startingFov)
        {
            cam.fieldOfView -= (-zoomSpeed * Time.deltaTime);
        }
    }
}
