using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuardPatrolState : State
{
    //public bool isAlerted = false;
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
            _destinationTracker.agent.enabled = false;
            _destinationTracker.enabled = false;
            return alertState; // The returned state will play in Update
        }
        else
        {
            _destinationTracker.agent.enabled = true;
            _destinationTracker.enabled = true;
            _destinationTracker.ActivateWaypoints();
            return this;
        }
    }
}
