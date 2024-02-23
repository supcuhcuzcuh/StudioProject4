using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseEnemy :  Entity
{
    public PlayerStats enemyStats;
    public Weapon enemyWeapon;
    public Rigidbody rb;
    public Animator enemyAnimator;

    [Header("Base Enemy Variables")]
    ////public float health;
    //public float hitsToDie;

    [SerializeField] private SecurityGuardPatrolState _patrolState;
    private StateManager _stateMachine;

    private void Start()
    {
        //_patrolState = trGetComponentInChildren<SecurityGuardPatrolState>();
        _stateMachine = GetComponent<StateManager>();
    }
    public override void OnDamaged(float damage)
    {
        if (_stateMachine.currentState == _patrolState)
        {
            _patrolState.damageInPatrol.Invoke();
        }
        base.OnDamaged(damage);
    }

    public void EnableWeaponPhysics()
    {
        enemyWeapon.GetComponent<Rigidbody>().useGravity = true;
        enemyWeapon.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void DisableWeaponPhysics()
    {
        enemyWeapon.GetComponent<Rigidbody>().useGravity = false;
        enemyWeapon.GetComponent<Rigidbody>().isKinematic = true;
    }
}
