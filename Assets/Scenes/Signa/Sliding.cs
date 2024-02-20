using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private PlayerStats playerStats;
    public Rigidbody rb;
    [SerializeField] Animator animator;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    public float slideTimer;

    public float slideYScale;
    private float startYScale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;

    private bool sliding = false;


    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;
    public float playerHeight;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //private void Update()
    //{
    //    horizontalInput = Input.GetAxisRaw("Horizontal");
    //    verticalInput = Input.GetAxisRaw("Vertical");

    //    if (Input.GetKeyDown(slideKey) && playerStats.currState == PlayerStats.PLAYERSTATES.SPRINT)
    //        StartSlide();

    //    if (Input.GetKeyUp(slideKey) && sliding)
    //        StopSlide();
    //}

    //private void FixedUpdate()
    //{
    //    if (sliding)
    //        SlidingMovement();
    //}

    private void StartSlide()
    {
        sliding = true;
        slideTimer = maxSlideTime;
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);     
    }

    public void SlidingUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(slideKey))
            StartSlide();

        if (Input.GetKeyUp(slideKey) && sliding)
            StopSlide();
    }

    public void SlidingFixedUpdate()
    {
        if (sliding)
        {
            Vector3 inputDirection = Camera.main.transform.forward * verticalInput + Camera.main.transform.right * horizontalInput;

            // sliding normal
            if (!OnSlope() || rb.velocity.y > -0.1f)
            {
                rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

                slideTimer -= Time.deltaTime;
            }

            // sliding down a slope
            else
            {
                rb.AddForce(GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
            }

            if (slideTimer <= 0)
                StopSlide();
        }
            
    }

    private void StopSlide()
    {
        if (playerStats.currAdditionalState == PlayerStats.ADDITIONALPLAYERSTATES.SLIDE)
        {
            playerStats.currAdditionalState = PlayerStats.ADDITIONALPLAYERSTATES.NONE;
        }
        sliding = false;
        animator.SetInteger("Crouch", 0);
    }


    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }
}
