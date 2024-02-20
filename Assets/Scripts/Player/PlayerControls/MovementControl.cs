using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveResponse
{
    void OnMove();
}

public class MovementControl : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;
    
    private Rigidbody rb;

    [SerializeField]
    private HeadbobShake headbobShake;
    [SerializeField]
    private GunBobShake gunbobShake;

    [SerializeField]
    private AudioManager audioManager;

    [SerializeField]
    private Animator animator;

    private List<IMoveResponse> moveResponses = new List<IMoveResponse>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void Movement()
    {
        //if (GetComponent<WallRunning>().pm) return;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = (Camera.main.transform.forward * verticalInput + Camera.main.transform.right * horizontalInput).normalized;
        moveDirection.y = 0;

        rb.MovePosition(transform.position + moveDirection * (playerStats.moveSpeed * playerStats.moveSpeedMultiplier) * Time.deltaTime);
       
       

        if (playerStats.currState != PlayerStats.PLAYERSTATES.SPRINT)
        {
            if ((Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0))
            {
                
                playerStats.currState = PlayerStats.PLAYERSTATES.WALK;
                audioManager.PlaySoundEffectLoop("Walk");
                animator.SetInteger("MoveCounter", 1);

                if(verticalInput > 0 && verticalInput > horizontalInput)
                {
                    animator.SetInteger("Dir", 0);  //forward dir
                    //Debug.Log("Forward");
                }
                else if(verticalInput < 0 && Mathf.Abs(verticalInput) > Mathf.Abs(horizontalInput))
                {
                    animator.SetInteger("Dir", 2);  //backwards dir
                    //Debug.Log("Backwards");
                }
                else if (horizontalInput > 0)
                {
                    animator.SetInteger("Dir", 1);  //right dir
                    //Debug.Log("Right");
                }
                else if (horizontalInput < 0)
                {
                    animator.SetInteger("Dir", 3);  //left dir
                    //Debug.Log("Left");
                }             
            }
            else if ((horizontalInput <= 0 && verticalInput <= 0))
            {
                playerStats.currState = PlayerStats.PLAYERSTATES.IDLE;
                audioManager.StopSoundEffect();
                animator.SetInteger("MoveCounter", 0);
            }
        }
    }

    private void NotifyOnMoveResponse()
    {
        foreach (var moveResponse in moveResponses)
        {
            moveResponse.OnMove();
        }
    }

    public void Subscribe(IMoveResponse moveResponse)
    {
        moveResponses.Add(moveResponse);
    }
}
