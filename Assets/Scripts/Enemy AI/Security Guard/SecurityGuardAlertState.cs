using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //private Renderer _rootRenderer; // Transform root
    private Color _defaultColor = Color.white;
    private void Start()
    {
        //_rootRenderer = transform.root.GetComponent<Renderer>();
        _timer = 0;
    }
    public override State PlayCurrentState()
    {
        if (!playerDetector.IsDetected())
        {
            //_rootRenderer.material.color = _defaultColor;
            return patrolState;
        }   
        else
        {
            _timer += Time.deltaTime;
            transform.root.LookAt(target.transform);
            transform.root.GetComponent<BaseEnemy>().rb.velocity = Vector3.zero;
            //_rootRenderer.material.color = Color.yellow;
            if (_timer >= toDefenseTime)
            {
                return defenseState;
            }
            return this;
        }
    }
}
