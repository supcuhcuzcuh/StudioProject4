using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class SecurityGuardPatrolState : State
{
    [HideInInspector] public UnityEvent damageInPatrol;
    [SerializeField] private RayDetector playerDetector;
    [SerializeField] private float fieldOfView;

    [Header("Potential States Of Transition")]
    public SecurityGuardAlertState alertState;
    public SecurityGuardDefenseState defenseState;
        
    [SerializeField] private WaypointsTracker destinationTracker;
    [SerializeField] private NavMeshAgent agent;
    private bool _damageInPatrolTracker = false;

    private float _timer;
    private void Start()
    {
        if (damageInPatrol == null)
        {
            damageInPatrol = new UnityEvent();
        }

        damageInPatrol.AddListener(delegate { DamagedWhilePatrol(); });

        agent = transform.root.GetComponent<NavMeshAgent>();
    }
    public override State PlayCurrentState()
    {
        _timer += Time.deltaTime * 200;
        playerDetector.toStart.localRotation = Quaternion.Euler( 0f , Mathf.PingPong(_timer, fieldOfView *  2) - fieldOfView, 0);


        if (enemy.GetHealth() <= 0.0f)
        {
            enemy.SetHealth(0.0f);  
            agent.enabled = false;
            destinationTracker.enabled = false;
            enemy.enemyWeapon.transform.parent = null;
            enemy.EnableWeaponPhysics();
            return deadState;
        }
        if (playerDetector.IsDetected())
        {
            Debug.Log("A");
            destinationTracker.enabled = false;
            return alertState; // The returned state will play in Update
        }
        else
        {

            if (_damageInPatrolTracker)
            {
                _damageInPatrolTracker = false;
                return defenseState;
            }

            destinationTracker.enabled = true;
            destinationTracker.ActivateWaypoints();  
            return this;
        }
    }

    private State DamagedWhilePatrol()
    {
        _damageInPatrolTracker = true;
        return defenseState;
    }
}
