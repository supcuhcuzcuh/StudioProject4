using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuardDefenseState : State
{
    [Header("Target / Player")]
    [SerializeField] private GameObject target;
    [Header("Potential States Of Transition")]
    public SecurityGuardAlertState alertState;
    public SecurityGuardDeadState deadState;
    public bool isDead = false;

    private BaseEnemy _enemyRef;
    private Renderer _rootRenderer; // Transform root
    private Color _defaultColor = Color.white;
    private WaypointsTracker _destinationTracker;

    private void Start()
    {
        _enemyRef = transform.root.GetComponent<BaseEnemy>();
        _rootRenderer = transform.root.GetComponent<Renderer>();
        _destinationTracker = transform.root.GetComponent<WaypointsTracker>();
    }
    public override State PlayCurrentState()
    {
        if (isDead)
        {
            return deadState;
        }
        else
        {
            // Logic code for this state
            //_destinationTracker.agent.SetDestination(new Vector3(0, 0, 0));
            transform.root.LookAt(target.transform);
            if (_enemyRef != null)
            {
                _enemyRef.rb.velocity = Vector3.zero;
            }
            _rootRenderer.material.color = Color.red;
            return this;
        }
    }
}
