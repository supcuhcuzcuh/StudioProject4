using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuardAlertState : State
{
    [Header("Potential States Of Transition")]
    public SecurityGuardPatrolState patrolState;
    public bool isOutOfSight;
    public override State PlayCurrentState()
    {
        if (isOutOfSight)
        {
            patrolState.isAlerted = false;
            return patrolState;
        }   
        else
        {
            Debug.Log("aff");
            return this;
        }
    }
}
