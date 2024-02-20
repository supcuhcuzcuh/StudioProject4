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
        if (transform.root.GetComponent<BaseEnemy>().GetHealth() <= 0.0f)
        {
            return deadState;
        }
        if (!playerDetector.IsDetected())
        {

            _timer = 0.0f;
            transform.root.GetComponent<BaseEnemy>().enemyAnimator.SetBool("isAlert", false);
            return patrolState;
        }   
        else
        {
            transform.root.GetComponent<BaseEnemy>().enemyAnimator.SetBool("isAlert", true);
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
