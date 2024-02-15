using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuardPatrolState : State
{
    [SerializeField] private WaypointsTracker waypointControl;

    [Header("Potential States Of Transition")]
    public SecurityGuardAlertState alertState;
    public bool isAlerted = false;
    public override State PlayCurrentState()
    {
        if (isAlerted)
        {
            return alertState; // The returned state will play in Update
        }
        else
        {
            waypointControl.ActivateWaypoints();
            //Debug.Log("In Patrol");
            return this;
        }
    }
}
