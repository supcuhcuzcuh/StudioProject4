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
    [SerializeField] private Transform trackerParent;

    private void Start()
    {
       // _enemyAnim = GameObject.FindWithTag("ENEMY").GetComponent<Animator>();
        _destinationTracker = transform.root.GetComponent<WaypointsTracker>();

        //if (_enemyAnim != null)
        //{
        //    _enemyAnim.SetBool("isPatrol", true);
        //}
        //else
        //{
        //    Debug.Log("jerald is lesbian | wow transgender represnstation");
        //}
    }
    public override State PlayCurrentState()
    {
        if (playerDetector.IsDetected())    
        {
           // _enemyAnim.SetBool("isPatrol", false);  
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
