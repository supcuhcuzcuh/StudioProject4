using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Camera Controls

public class FPSCameraController : MonoBehaviour
{
    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;
    public float mouseSensitivity = 2.0f;


    //private GameObject player;
    private WallRunning wallRunningScript;
    private bool debugLock;

    // Start is called before the first frame update
    void Start()
    {
        wallRunningScript = GetComponentInParent<WallRunning>(); // Assuming the WallRunning script is on the same GameObject or the parent
        debugLock = false;  //debugging tool, presses escape to lock the player's view
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        HandleCameraRotation();
        if (Input.GetKeyDown("escape"))
        {
            if(debugLock == false)
            {
                debugLock = true;
            }
            else
            {
                debugLock = false;
            }
            
        }
    }

    private void HandleCameraRotation()
    {
         if (!debugLock && (wallRunningScript == null || !wallRunningScript.pm))
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity; // Invert Y-axis for more intuitive control

            horizontalRotation += mouseX;

            verticalRotation = Mathf.Clamp(verticalRotation + mouseY, -90, 90);

            // Rotate around X and Y axes only
            transform.Rotate(0, mouseX, 0);
            Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
        }
        //if(debugLock == false)
        //{
        //    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        //    horizontalRotation += mouseX;

        //    verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        //    verticalRotation = Mathf.Clamp(verticalRotation, -90, 90);

        //    transform.Rotate(0, mouseX, 0);
        //    Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);

        //}
    }
   
}
