using UnityEngine;
using System.Threading.Tasks;
public class SecurityGuardDefenseState : State
{
    [SerializeField] private GameObject root;
    [SerializeField] private RayDetector playerDetector;
    [SerializeField] private WaitForReload reloadEvent;
    [Header("Target / Player")]
    [SerializeField] private GameObject target;
    [Header("Potential States Of Transition")]
    public SecurityGuardAlertState alertState;
    public SecurityGuardPatrolState patrolState;
    public bool isDead = false;

    [SerializeField] private WaypointsTracker destinationTracker;
    private RayDetector _rayDetector;

    private float _nextTimeToShoot;

    //[Header("DEBUG")]
    //public TMPro.TMP_Text distFromPlayerText;
    //public TMPro.TMP_Text enemyAmmoText;

    private void Start()
    {
        _rayDetector = GetComponent<RayDetector>();
    }
    public override State PlayCurrentState()
    {
        float distFromPlayer = Vector3.Distance(target.transform.position, root.transform.position);
        //if (distFromPlayerText.text != null)
        //{
        //    distFromPlayerText.text = "DISTANCE FROM PLAYER: " + distFromPlayer;
        //}

        if (enemy.GetHealth() <= 0.0f)
        {
            enemy.SetHealth(0.0f);
            enemy.enemyAnimator.SetBool("isDefense", false);
            enemy.enemyWeapon.transform.parent = null;
            enemy.EnableWeaponPhysics();
            return deadState;
        }
        if (distFromPlayer > 13.0f)
        {
            enemy.enemyAnimator.SetBool("isDefense", false);
            enemy.enemyAnimator.SetBool("isStandShooting", false);
            enemy.enemyAnimator.SetBool("isAlert", false);
            enemy.enemyAnimator.SetBool("isReload", false);
            enemy.enemyAnimator.SetBool("isPatrol", true);
            enemy.enemyAnimator.CrossFade("Guard_Walk", 0.5f);
            destinationTracker.ActivateWaypoints();
            //enemy.enemyAnimator.SetBool("isAlert", false);
            //enemy.enemyAnimator.SetBool("isDefense", false);
            //enemy.enemyAnimator.CrossFade("Guard_Walk", 0.5f);
            //destinationTracker.ActivateWaypoints();
            return patrolState;
        }
        else
        {
            //if (!playerDetector.IsDetected())
            //{
            //    enemy.enemyAnimator.SetBool("isDefense", false);
            //    enemy.enemyAnimator.SetBool("isStandShooting", false);
            //    enemy.enemyAnimator.SetBool("isAlert", false);
            //    enemy.enemyAnimator.SetBool("isReload", false);
            //    enemy.enemyAnimator.SetBool("isPatrol", true);
            //    enemy.enemyAnimator.CrossFade("Guard_Walk", 0.5f);
            //    //enemy.enemyAnimator.SetBool("isAlert", false);
            //    destinationTracker.ActivateWaypoints();
            //    return patrolState;
            //}
            enemy.enemyAnimator.SetBool("isDefense", true);
            //enemyAmmoText.text = "ENEMY AMMO: " + enemy.enemyWeapon.clipSizeCurr;
            var targetPos = target.transform.position;
            targetPos.y = transform.position.y;
            transform.root.LookAt(targetPos);
            Debug.Log("TARGET IS : " + target.name);

            destinationTracker.agent.SetDestination(target.transform.position);

            if (enemy != null)
            {       
                enemy.rb.velocity = Vector3.zero;
            }

            if (distFromPlayer < 5.0f) // Check if enemy close to play, then play stand shooting animation
            {
                destinationTracker.agent.ResetPath(); // Stop enemy from moving 

                if ( Time.time > _nextTimeToShoot)  //Main action for shooting
                {
                    // set cooldown delay
                    if (!reloadEvent.isReloading)
                    {
                        enemy.enemyWeapon.OnMouse1();
                    }
                    if (enemy.enemyWeapon.clipSizeCurr <= 0)
                    {
                        enemy.enemyAnimator.SetTrigger("isReload");
                        enemy.enemyWeapon.clipSizeCurr = enemy.enemyStats.playerAmmo;
                        Debug.Log("ENENMY STAT PLAYER AMMO IS : " + enemy.enemyStats.playerAmmo);
                    }
                    _nextTimeToShoot = Time.time + enemy.enemyWeapon.cooldownWindow;
                }

                enemy.enemyAnimator.SetBool("isStandShooting", true);
            }   
            else 
            {
                enemy.enemyAnimator.SetBool("isStandShooting", false);
            }
            return this;
 
        }
    }
}
