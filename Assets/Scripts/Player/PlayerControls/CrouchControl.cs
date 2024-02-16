using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchControl : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    private BoxCollider col;

    private float crouchCameraHeightMultiplier; //how much to reduce the camera height and hitbox size by
    private float crouchHitboxMultiplier;

    [SerializeField] Animator animator;
    [SerializeField] private GameObject rigs;
    [SerializeField] private GameObject modelOffset;

    // Start is called before the first frame update
    void Start()
    {
        playerStats.currAdditionalState = PlayerStats.ADDITIONALPLAYERSTATES.NONE;
        col = GetComponent<BoxCollider>();
        crouchCameraHeightMultiplier = 0.4f;
        crouchHitboxMultiplier = 1.75f;
    }

    public void ActivateCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && (playerStats.currState != PlayerStats.PLAYERSTATES.JUMP || playerStats.currAdditionalState != PlayerStats.ADDITIONALPLAYERSTATES.SLIDE))
        {
            if(playerStats.currState == PlayerStats.PLAYERSTATES.SPRINT)
            {
                playerStats.currAdditionalState = PlayerStats.ADDITIONALPLAYERSTATES.SLIDE;
                animator.SetInteger("Crouch", 0);
            }
            else if (playerStats.currAdditionalState == PlayerStats.ADDITIONALPLAYERSTATES.CROUCH)
            {
                Uncrouch();
                playerStats.currAdditionalState = PlayerStats.ADDITIONALPLAYERSTATES.NONE;

                animator.SetInteger("Crouch", 0);
            }
            else if (playerStats.currAdditionalState == PlayerStats.ADDITIONALPLAYERSTATES.PRONE)
            {
                Uncrouch();
                playerStats.currAdditionalState = PlayerStats.ADDITIONALPLAYERSTATES.CROUCH;
            }
            else if (playerStats.currState == PlayerStats.PLAYERSTATES.WALK || playerStats.currState == PlayerStats.PLAYERSTATES.IDLE)
            {
                Crouch();
                playerStats.currAdditionalState = PlayerStats.ADDITIONALPLAYERSTATES.CROUCH;            
                animator.SetInteger("Crouch", 1);
            }                    
        }
        //else if (Input.GetKeyDown(KeyCode.C) && playerStats.currState != PlayerStats.PLAYERSTATES.JUMP)   //going prone
        //{
        //    if (playerStats.currAdditionalState == PlayerStats.ADDITIONALPLAYERSTATES.CROUCH)
        //    {
        //        Crouch();
        //        playerStats.currAdditionalState = PlayerStats.ADDITIONALPLAYERSTATES.PRONE;
        //    }
        //    else if (playerStats.currAdditionalState == PlayerStats.ADDITIONALPLAYERSTATES.PRONE)
        //    {
        //        Uncrouch();
        //        Uncrouch();
        //        playerStats.currAdditionalState = PlayerStats.ADDITIONALPLAYERSTATES.NONE;
        //    }
        //    else if (playerStats.currState == PlayerStats.PLAYERSTATES.WALK || playerStats.currState == PlayerStats.PLAYERSTATES.IDLE)
        //    {
        //        Debug.Log("Prone");
        //        Crouch();
        //        Crouch();
        //        playerStats.currAdditionalState = PlayerStats.ADDITIONALPLAYERSTATES.PRONE;
        //    }          
        //}
    }

    void Crouch()
    {
        //Move the camera down             
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - crouchCameraHeightMultiplier, Camera.main.transform.position.z);
        rigs.transform.position = new Vector3(rigs.transform.position.x, rigs.transform.position.y - crouchCameraHeightMultiplier, rigs.transform.position.z);
        modelOffset.transform.position = new Vector3(modelOffset.transform.position.x, modelOffset.transform.position.y + crouchCameraHeightMultiplier, modelOffset.transform.position.z);

        //Reduce hitbox size
        col.center = new Vector3(col.center.x, col.center.y / crouchHitboxMultiplier, col.center.z);
        col.size = new Vector3(col.size.x, col.size.y / crouchHitboxMultiplier, col.size.z);
    }

    void Uncrouch()
    {
        //Move the camera up
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + crouchCameraHeightMultiplier, Camera.main.transform.position.z); 
        rigs.transform.position = new Vector3(rigs.transform.position.x, rigs.transform.position.y + crouchCameraHeightMultiplier, rigs.transform.position.z);
        modelOffset.transform.position = new Vector3(modelOffset.transform.position.x, modelOffset.transform.position.y - crouchCameraHeightMultiplier, modelOffset.transform.position.z);

        //Return hitbox to original size
        col.center = new Vector3(col.center.x, col.center.y * crouchHitboxMultiplier, col.center.z);
        col.size = new Vector3(col.size.x, col.size.y * crouchHitboxMultiplier, col.size.z);
    }
}
