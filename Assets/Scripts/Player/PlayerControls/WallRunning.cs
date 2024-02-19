using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("Wallrunning")]
    public LayerMask whatIsGround;
    public float wallRunForce;
    public float wallClimbSpeed;
    public float maxWallRunTime;
    private float wallRunTimer;

    [Header("Input")]
    public KeyCode upwardsRunKey = KeyCode.LeftShift;
    public KeyCode downwardsRunKey = KeyCode.LeftControl;
    private bool upwardsRunning;
    private bool downwardsRunning;
    private float horizontalInput;
    private float verticalInput;

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallLeft;
    private bool wallRight;

    [Header("References")]
    public Transform orientation;
    public Transform cameraTransform; // Reference to your camera transform
    [HideInInspector] public bool pm;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }

    private void FixedUpdate()
    {
        StateMachine();
        //CheckForWall();
        if (pm)
        {
            WallRunningMovement();
            // Rotate the camera based on wall running direction
            //RotateCamera();
        }

        // Check for wall jump input
        if (Input.GetButtonDown("Jump") && pm)
        {
            WallJump();
        }
    }

    private void CheckForWall()
    {
        if (Physics.Raycast(transform.position, Camera.main.transform.right, out rightWallhit, wallCheckDistance))
        {
            if (rightWallhit.collider.gameObject.tag == "Wall")
            {
                wallRight = true;
            }
        }

        if (Physics.Raycast(transform.position, -Camera.main.transform.right, out leftWallhit, wallCheckDistance))
        {
            if (leftWallhit.collider.gameObject.tag == "Wall")
            {
                wallLeft = true;
            }
        }

        Debug.Log("Left: " + wallLeft + " Right: " + wallRight);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void RotateCamera()
    {
        Quaternion targetRotation;

        if (wallRight)
        {
            // Rotate based on mouse input along the y-axis
            float mouseY = Input.GetAxis("Mouse Y") * 2;
            targetRotation = Quaternion.Euler(cameraTransform.rotation.eulerAngles.x, cameraTransform.rotation.eulerAngles.y + mouseY, 30f);
        }
        else if (wallLeft)
        {
            // Rotate based on mouse input along the y-axis
            float mouseY = Input.GetAxis("Mouse Y") * 2;
            targetRotation = Quaternion.Euler(cameraTransform.rotation.eulerAngles.x, cameraTransform.rotation.eulerAngles.y + mouseY, -30f);
        }
        else
        {
            targetRotation = Quaternion.Euler(cameraTransform.rotation.eulerAngles.x, cameraTransform.rotation.eulerAngles.y, 0f); ; // No rotation if not wall running
        }

        // Smoothly interpolate between the current rotation and the target rotation
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    private void StateMachine()
    {
        // Getting Inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //upwardsRunning = Input.GetKey(upwardsRunKey);
        //downwardsRunning = Input.GetKey(downwardsRunKey);

        // State 1 - Wallrunning
        if((wallLeft || wallRight) && verticalInput > 0 && AboveGround())
        {
            if (!pm)
            {
                Debug.Log("Start wall run");
                StartWallRun();         
            }
        }

        // State 3 - None
        else
        {
            if (pm)
            {
                Debug.Log("Stop wall run");
                StopWallRun();      
            }
        }
    }

    private void StartWallRun()
    {
        pm = true;
    }

    private void WallRunningMovement()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((Camera.main.transform.forward - wallForward).magnitude > (Camera.main.transform.forward - -wallForward).magnitude)
            wallForward = -wallForward;

        // forward force
        rb.AddForce(wallForward * wallRunForce);

        //// upwards/downwards force
        //if (upwardsRunning)
        //    rb.velocity = new Vector3(rb.velocity.x, wallClimbSpeed, rb.velocity.z);
        //if (downwardsRunning)
        //    rb.velocity = new Vector3(rb.velocity.x, -wallClimbSpeed, rb.velocity.z);


    }

    private void WallJump()
    {
        pm = false; // Disable wall running state
        rb.useGravity = true;

        // Calculate wall jump direction (opposite of the wall normal)
        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
        Vector3 wallJumpDirection = wallNormal; // Opposite direction

        Debug.Log("Jumped");
        // Apply wall jump force
        rb.AddForce(wallJumpDirection * wallRunForce * 1.5f, ForceMode.Impulse); // Adjust the multiplier as needed
    }

    private void StopWallRun()
    {
        pm = false;
        rb.useGravity = true;
    }

    void OnCollisionExit(Collision col)
    {
        if(col.gameObject.tag == "Wall")
        {
            Debug.Log("Leave wall");         
            wallLeft = false;
            wallRight = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (Physics.Raycast(transform.position, Camera.main.transform.right, out rightWallhit, wallCheckDistance))
        {
            if (rightWallhit.collider.gameObject.tag == "Wall")
            {
                wallRight = true;
            }
        }

        if (Physics.Raycast(transform.position, -Camera.main.transform.right, out leftWallhit, wallCheckDistance))
        {
            if (leftWallhit.collider.gameObject.tag == "Wall")
            {
                wallLeft = true;
            }
        }
    }

}
