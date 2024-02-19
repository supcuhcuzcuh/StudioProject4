using UnityEngine;

public class SecurityGuardDefenseState : State
{
    [Header("Target / Player")]
    [SerializeField] private GameObject target;
    [Header("Potential States Of Transition")]
    public SecurityGuardAlertState alertState;
    public SecurityGuardDeadState deadState;
    public SecurityGuardPatrolState patrolState;
    public bool isDead = false;

    private BaseEnemy _enemyRef;
    private WaypointsTracker _destinationTracker;
    private RayDetector _rayDetector;

    private float _nextTimeToShoot;

    [Header("DEBUG")]
    public TMPro.TMP_Text distFromPlayerText;
    private void Start()
    {
        _enemyRef = transform.root.GetComponent<BaseEnemy>();
        _destinationTracker = transform.root.GetComponent<WaypointsTracker>();
        _rayDetector = GetComponent<RayDetector>();
    }
    public override State PlayCurrentState()
    {
        float distFromPlayer = Vector3.Distance(target.transform.position, transform.root.transform.position);
        distFromPlayerText.text = "DISTANCE FROM PLAYER: " + distFromPlayer;
        if (isDead)
        {
            transform.root.GetComponent<BaseEnemy>().enemyAnimator.SetBool("isDefense", false);
            return deadState;
        }
        else if (distFromPlayer > 25.0f)
        {
            transform.root.GetComponent<BaseEnemy>().enemyAnimator.SetBool("isDefense", false);
            return patrolState;
        }
        else
        {
            transform.root.GetComponent<BaseEnemy>().enemyAnimator.SetBool("isDefense", true);
            _destinationTracker.agent.SetDestination(target.transform.position);

            if (_rayDetector)
            {
                Debug.Log("SHOOTING PLAYING");
            }

            if (_enemyRef != null)
            {
                _enemyRef.rb.velocity = Vector3.zero;
            }

            if (distFromPlayer < 4.0f) // Check if enemy close to play, then play stand shooting animation
            {
                _destinationTracker.agent.ResetPath();

                if ( Time.time > _nextTimeToShoot)     //Main action for shooting
                {
                    // set cooldown delay
                    transform.root.GetComponent<BaseEnemy>().enemyWeapon.OnMouse1();
                    _nextTimeToShoot = Time.time + transform.root.GetComponent<BaseEnemy>().enemyWeapon.cooldownWindow;
                }

                transform.root.GetComponent<BaseEnemy>().enemyAnimator.SetBool("isStandShooting", true);
            }   
            else 
            {
                transform.root.GetComponent<BaseEnemy>().enemyAnimator.SetBool("isStandShooting", false);
            }
            return this;
        }
    }
}
