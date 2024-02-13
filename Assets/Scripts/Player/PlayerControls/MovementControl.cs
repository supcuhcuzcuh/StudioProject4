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
    

    private List<IMoveResponse> moveResponses = new List<IMoveResponse>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void Movement()
    {
       
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = (Camera.main.transform.forward * verticalInput + Camera.main.transform.right * horizontalInput).normalized;
        moveDirection.y = 0;
        rb.MovePosition(transform.position + moveDirection * (playerStats.moveSpeed * playerStats.moveSpeedMultiplier) * Time.deltaTime);

        if ((horizontalInput > 0 || verticalInput > 0))
        {
            audioManager.PlaySoundEffectLoop("Walk");
        }
        else if((horizontalInput <= 0 && verticalInput <= 0))
        {
            audioManager.StopSoundEffect();
        }


        //if (horizontalInput != 0 || verticalInput != 0)
        //{
        //    headbobShake.StartShake();
        //    gunbobShake.StartShake();
        //}      
        //else
        //{
        //    headbobShake.EndShake();
        //    gunbobShake.EndShake();
        //}      
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
