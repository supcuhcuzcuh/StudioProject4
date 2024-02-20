using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatesManager : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats.currState = PlayerStats.PLAYERSTATES.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStats.currState == PlayerStats.PLAYERSTATES.WALK)
        {
            if (playerStats.currAdditionalState == PlayerStats.ADDITIONALPLAYERSTATES.CROUCH)
            {
                playerStats.moveSpeedMultiplier = 0.3f;
            }
            else if(playerStats.currAdditionalState == PlayerStats.ADDITIONALPLAYERSTATES.PRONE)
            {
                playerStats.moveSpeedMultiplier = 0.15f;
            }
            else
            {
                playerStats.moveSpeedMultiplier = 1.0f;
            }        
        }
        else if(playerStats.currState == PlayerStats.PLAYERSTATES.SPRINT)
        {
            playerStats.moveSpeedMultiplier = 1.75f;
        }    
    }
}
