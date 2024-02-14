using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{

    [SerializeField]
    private PlayerStats playerStats;

    [SerializeField] GameObject playerModelOffset;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void  PlayerAnimationUpdate()
    {
        playerModelOffset.transform.forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);


        if (playerStats.currState == PlayerStats.PLAYERSTATES.IDLE)
        {
            animator.SetInteger("MoveCounter", 0);
        }
        else if (playerStats.currState == PlayerStats.PLAYERSTATES.WALK)
        {
            animator.SetInteger("MoveCounter", 1);
        }
        else if (playerStats.currState == PlayerStats.PLAYERSTATES.SPRINT)
        {
            animator.SetInteger("MoveCounter", 2);
        }
        else if (playerStats.currState == PlayerStats.PLAYERSTATES.JUMP)
        {
            animator.SetInteger("MoveCounter", 3);
        }     
    }
}
