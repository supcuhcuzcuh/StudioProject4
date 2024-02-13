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



    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider>();
        crouchCameraHeightMultiplier = 0.4f;
        crouchHitboxMultiplier = 1.75f;
    }

    public void ActivateCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && playerStats.currState != PlayerStats.PLAYERSTATES.JUMP)
        {
            if (playerStats.currState == PlayerStats.PLAYERSTATES.WALK)
            {
                Crouch();
                playerStats.currState = PlayerStats.PLAYERSTATES.CROUCH;
            }
            else if (playerStats.currState == PlayerStats.PLAYERSTATES.CROUCH)
            {
                Uncrouch();
                playerStats.currState = PlayerStats.PLAYERSTATES.WALK;
            }
            else if(playerStats.currState == PlayerStats.PLAYERSTATES.PRONE)
            {
                Uncrouch();
                playerStats.currState = PlayerStats.PLAYERSTATES.CROUCH;
            }                     

        }
        else if (Input.GetKeyDown(KeyCode.C) && playerStats.currState != PlayerStats.PLAYERSTATES.JUMP)
        {
            if (playerStats.currState == PlayerStats.PLAYERSTATES.WALK)
            {
                Debug.Log("Prone");
                Crouch();
                Crouch();
                playerStats.currState = PlayerStats.PLAYERSTATES.PRONE;
            }
            else if (playerStats.currState == PlayerStats.PLAYERSTATES.CROUCH)
            {
                Crouch();
                playerStats.currState = PlayerStats.PLAYERSTATES.PRONE;
            }
            else if (playerStats.currState == PlayerStats.PLAYERSTATES.PRONE)
            {
                Uncrouch();
                Uncrouch();
                playerStats.currState = PlayerStats.PLAYERSTATES.WALK;
            }            
        }

        //if (currState == STATE.NONE)
        //{
        //    playerStats.moveSpeedMultiplier = 1.0f;
        //    playerStats.canJump = true;
        //}
        //else if (currState == STATE.CROUCH)
        //{
        //    playerStats.moveSpeedMultiplier = 0.3f;
        //    playerStats.canJump = false;
        //}
        //else if (currState == STATE.PRONE)
        //{
        //    playerStats.moveSpeedMultiplier = 0.15f;
        //    playerStats.canJump = false;
        //}
    }

    void Crouch()
    {
        //Move the camera down             
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - crouchCameraHeightMultiplier, Camera.main.transform.position.z);

        //Reduce hitbox size
        col.center = new Vector3(col.center.x, col.center.y / crouchHitboxMultiplier, col.center.z);
        col.size = new Vector3(col.size.x, col.size.y / crouchHitboxMultiplier, col.size.z);
    }

    void Uncrouch()
    {
        //Move the camera up
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + crouchCameraHeightMultiplier, Camera.main.transform.position.z); ;

        //Return hitbox to original size
        col.center = new Vector3(col.center.x, col.center.y * crouchHitboxMultiplier, col.center.z);
        col.size = new Vector3(col.size.x, col.size.y * crouchHitboxMultiplier, col.size.z);
    }
}
