using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class SecurityGuardPatrolState : State
{
    [HideInInspector] public UnityEvent damageInPatrol;
    [SerializeField] private RayDetector playerDetector;

    [Header("Potential States Of Transition")]
    public SecurityGuardAlertState alertState;
    public SecurityGuardDefenseState defenseState;

    private WaypointsTracker _destinationTracker;
    private NavMeshAgent _agent;
    private bool _damageInPatrolTracker = false;
    private void Start()
    {
        _destinationTracker = transform.root.GetComponent<WaypointsTracker>();

        if (damageInPatrol == null)
        {
            damageInPatrol = new UnityEvent();
        }

        damageInPatrol.AddListener(delegate { DamagedWhilePatrol(); });

        _agent = transform.root.GetComponent<NavMeshAgent>();
    }
    public override State PlayCurrentState()
    {
        if (transform.root.GetComponent<BaseEnemy>().GetHealth() <= 0.0f)
        {
            transform.root.GetComponent<BaseEnemy>().SetHealth(0.0f);
            _agent.enabled = false;
            _destinationTracker.enabled = false;
            transform.root.GetComponent<BaseEnemy>().enemyWeapon.transform.parent = null;
            transform.root.GetComponent<BaseEnemy>().EnableWeaponPhysics();
            return deadState;
        }
        //if (playerDetector.IsDetected())    
        //{
        //    _destinationTracker.enabled = false;
        //    return alertState; // The returned state will play in Update
        //}
        if (playerDetector.IsSurroundingDetected())
        {
            _destinationTracker.enabled = false;

            return alertState; // The returned state will play in Update
        }
        else
        {
            if (_damageInPatrolTracker)
            {
                return defenseState;
            }

            _destinationTracker.enabled = true;
            _destinationTracker.ActivateWaypoints();  
            return this;
        }
    }

    private State DamagedWhilePatrol()
    {
        _damageInPatrolTracker = true;
        return defenseState;
    }
}
