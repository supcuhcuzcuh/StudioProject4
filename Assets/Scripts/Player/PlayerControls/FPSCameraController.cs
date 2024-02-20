using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Camera Controls

public class FPSCameraController : MonoBehaviour
{
    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;
    public float mouseSensitivity = 2.0f;

    [SerializeField] GameObject playerModelOffset;
    [SerializeField] Transform headPos;

    [SerializeField]
    private PlayerStats playerStats;

    Quaternion rotation;

    //private GameObject player;
    private bool debugLock = false;  //debugging tool, presses escape to lock the player's view
    // Start is called before the first frame update
    void Start()
    {    
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
        //if (!debugLock && (wallRunning == null || !wallRunning.pm))
        //{
        //    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        //    float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity; // Invert Y-axis for more intuitive control

        //    horizontalRotation += mouseX;

        //    verticalRotation = Mathf.Clamp(verticalRotation + mouseY, -90, 90);

        //    // Rotate around X and Y axes only
        //    transform.Rotate(0, mouseX, 0);
        //    Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
        //    playerModelOffset.transform.forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        //}
        if (debugLock == false)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            horizontalRotation += mouseX;

            verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -85, 85);

            transform.Rotate(0, mouseX, 0);
            Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
            Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, headPos.position, 10 * Time.deltaTime);

            if (playerStats.currAdditionalState != PlayerStats.ADDITIONALPLAYERSTATES.SLIDE)
            {
                playerModelOffset.transform.forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            }
            else        //because no sliding model exsist, have to take another model and transform it to fit
            {
                playerModelOffset.transform.localPosition = new Vector3(-0.6f, playerModelOffset.transform.localPosition.y, -0.255f);

                //rotation.eulerAngles = new Vector3(0, -90, 0);
                //playerModelOffset.transform.localRotation = rotation;
                playerModelOffset.transform.forward = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);
            }

        }
    }
}
