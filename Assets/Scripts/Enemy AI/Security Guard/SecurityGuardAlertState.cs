using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecurityGuardAlertState : State
{
    [Header("Target to detect during raycast")]
    [SerializeField] private RayDetector playerDetector;
    [SerializeField] private GameObject target;

    [Header("Time for player to get into cover")]
    public float _timer;
    [SerializeField] private float toDefenseTime;

    [Header("Potential States Of Transition")]
    public SecurityGuardPatrolState patrolState;
    public SecurityGuardDefenseState defenseState;
    public bool isOutOfSight;

    private WaypointsTracker _destinationTracker;

    private void Start()
    {
        _timer = 0;
        _destinationTracker = transform.root.GetComponent<WaypointsTracker>();
    }
    public override State PlayCurrentState()
    {
        if (!playerDetector.IsDetected())
        {
            _timer = 0.0f;
            return patrolState;
        }   
        else
        {
            _timer += Time.deltaTime * 2.0f;
            _destinationTracker.agent.ResetPath(); // Stop current path
            transform.root.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (_timer >= toDefenseTime)    
            {
                return defenseState;
            }
            return this;
        }
    }
}
