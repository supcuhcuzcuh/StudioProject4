using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecurityGuardAlertState : State
{
    [SerializeField] private GameObject root;
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

    [SerializeField] private WaypointsTracker destinationTracker;
    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        _timer = 0;
    }
    public override State PlayCurrentState()
    {
        if (enemy.GetHealth() <= 0.0f)
        {
            enemy.enemyWeapon.transform.parent = null;
            enemy.EnableWeaponPhysics();
            return deadState;
        }
        if (!playerDetector.IsDetected())
        {
            _timer = 0.0f;
            enemy.enemyAnimator.SetBool("isAlert", false);
            enemy.enemyAnimator.CrossFade("Guard_Walk", 0.5f);
            return patrolState;
        }
        //if (!playerDetector.IsSurroundingDetected())
        //{
        //    _timer = 0.0f;
        //    enemy.enemyAnimator.SetBool("isAlert", false);
        //    return patrolState;
        //}
        else
        {
            float distFromPlayer = Vector3.Distance(target.transform.position, root.transform.position);
            enemy.enemyAnimator.SetBool("isAlert", true);
            _timer += Time.deltaTime * 2.0f;
            destinationTracker.agent.ResetPath(); // Stop current path
            rb.velocity = Vector3.zero;
            if (_timer >= toDefenseTime)    
            {
                if (distFromPlayer > 5.0f)
                {
                    enemy.enemyAnimator.SetBool("isStandShooting", true);
                }
                else
                {
                    enemy.enemyAnimator.SetBool("isDefense", true);
                    //enemy.enemyAnimator.SetBool("isAlert", false);
                }   
                return defenseState;
            }
            return this;
        }
    }
}
