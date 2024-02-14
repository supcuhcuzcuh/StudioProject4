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
    private bool debugLock;

    // Start is called before the first frame update
    void Start()
    {
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
        if(debugLock == false)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            horizontalRotation += mouseX;

            verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -85, 85);

            transform.Rotate(0, mouseX, 0);
            Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);

        }
    }
   
}
