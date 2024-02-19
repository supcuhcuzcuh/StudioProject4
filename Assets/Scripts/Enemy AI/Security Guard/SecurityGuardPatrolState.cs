using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuardPatrolState : State
{
    private Animator _enemyAnim;
    [SerializeField] private RayDetector playerDetector;
    [Header("Debug")]
    [Header("Potential States Of Transition")]
    public SecurityGuardAlertState alertState;

    private WaypointsTracker _destinationTracker;

    private void Start()
    {
        _destinationTracker = transform.root.GetComponent<WaypointsTracker>();
    }
    public override State PlayCurrentState()
    {
        if (playerDetector.IsDetected())    
        {
            _destinationTracker.enabled = false;
            return alertState; // The returned state will play in Update
        }
        else
        {
            _destinationTracker.enabled = true;
            _destinationTracker.ActivateWaypoints();  
            return this;
        }
    }
}
