using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpControl : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerStats.canJump == true && playerStats.currState != PlayerStats.PLAYERSTATES.CROUCH && playerStats.currState != PlayerStats.PLAYERSTATES.PRONE)
        {
            playerStats.canJump = false;
            rb.AddForce((transform.up)* playerStats.jumpForce, ForceMode.Impulse);
            playerStats.currState = PlayerStats.PLAYERSTATES.JUMP;
        }
    }



    void OnCollisionEnter(Collision collision)
    {
        if (playerStats.canJump == false)
        {
            playerStats.canJump = true;
            playerStats.currState = PlayerStats.PLAYERSTATES.WALK;
        }
    }
}
