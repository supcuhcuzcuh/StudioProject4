using UnityEngine;
using System.Threading.Tasks;
public class SecurityGuardDefenseState : State
{
    [SerializeField] private WaitForReload reloadEvent;
    [Header("Target / Player")]
    [SerializeField] private GameObject target;
    [Header("Potential States Of Transition")]
    public SecurityGuardAlertState alertState;
    public SecurityGuardPatrolState patrolState;
    public bool isDead = false;

    private BaseEnemy _enemyRef;
    private WaypointsTracker _destinationTracker;
    private RayDetector _rayDetector;

    private float _nextTimeToShoot;

    [Header("DEBUG")]
    public TMPro.TMP_Text distFromPlayerText;
    public TMPro.TMP_Text enemyAmmoText;

    private void Start()
    {
        _enemyRef = transform.root.GetComponent<BaseEnemy>();
        _destinationTracker = transform.root.GetComponent<WaypointsTracker>();
        _rayDetector = GetComponent<RayDetector>();
    }
    public override State PlayCurrentState()
    {
        float distFromPlayer = Vector3.Distance(target.transform.position, transform.root.transform.position);
        if (distFromPlayerText.text != null)
        {
            distFromPlayerText.text = "DISTANCE FROM PLAYER: " + distFromPlayer;
        }

        if (transform.root.GetComponent<BaseEnemy>().GetHealth() <= 0.0f)
        {
            transform.root.GetComponent<BaseEnemy>().SetHealth(0.0f);
            transform.root.GetComponent<BaseEnemy>().enemyAnimator.SetBool("isDefense", false);
            transform.root.GetComponent<BaseEnemy>().enemyWeapon.transform.parent = null;
            transform.root.GetComponent<BaseEnemy>().EnableWeaponPhysics();
            return deadState;
        }
        else if (distFromPlayer > 25.0f)
        {
            transform.root.GetComponent<BaseEnemy>().enemyAnimator.SetBool("isDefense", false);
            return patrolState;
        }
        else
        {
            Debug.Log(transform.root.GetComponent<BaseEnemy>().enemyWeapon.clipSizeCurr);
            enemyAmmoText.text = "ENEMY AMMO: " + transform.root.GetComponent<BaseEnemy>().enemyWeapon.clipSizeCurr;
            var targetPos = target.transform.position;
            targetPos.y = transform.position.y;
            transform.root.LookAt(targetPos);
            Debug.Log("TARGET IS : " + target.name);
            _destinationTracker.agent.SetDestination(target.transform.position);

            transform.root.GetComponent<BaseEnemy>().enemyAnimator.SetBool("isDefense", true);

            if (_rayDetector)
            {
                Debug.Log("SHOOTING PLAYING");
            }

            if (_enemyRef != null)
            {
                _enemyRef.rb.velocity = Vector3.zero;
            }

            if (distFromPlayer < 5.0f) // Check if enemy close to play, then play stand shooting animation
            {
                _destinationTracker.agent.ResetPath(); // Stop enemy from moving

                if ( Time.time > _nextTimeToShoot)  //Main action for shooting
                {
                    // set cooldown delay
                    if (!reloadEvent.isReloading)
                    {
                        transform.root.GetComponent<BaseEnemy>().enemyWeapon.OnMouse1();
                    }
                    if (transform.root.GetComponent<BaseEnemy>().enemyWeapon.clipSizeCurr <= 0)
                    {
                        transform.root.GetComponent<BaseEnemy>().enemyAnimator.SetTrigger("isReload");
                        transform.root.GetComponent<BaseEnemy>().enemyWeapon.clipSizeCurr = transform.root.GetComponent<BaseEnemy>().enemyStats.playerAmmo;
                        Debug.Log("ENENMY STAT PLAYER AMMO IS : " + transform.root.GetComponent<BaseEnemy>().enemyStats.playerAmmo);
                    }
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
