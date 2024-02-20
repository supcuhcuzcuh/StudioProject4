using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpControl : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField] Animator animator;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerStats.canJump == true && playerStats.currAdditionalState == PlayerStats.ADDITIONALPLAYERSTATES.NONE)
        {
            playerStats.canJump = false;
            rb.AddForce((transform.up)* playerStats.jumpForce + (transform.forward * playerStats.moveSpeedMultiplier * 0.5f), ForceMode.Impulse);
            playerStats.currAdditionalState = PlayerStats.ADDITIONALPLAYERSTATES.JUMP;
            animator.SetInteger("MoveCounter", 3);
        }
    }



    void OnCollisionEnter(Collision collision)
    {
        if (playerStats.canJump == false && collision.gameObject.tag != "Wall")
        {
            playerStats.canJump = true;
            playerStats.currAdditionalState = PlayerStats.ADDITIONALPLAYERSTATES.NONE;
        }
    }
}
