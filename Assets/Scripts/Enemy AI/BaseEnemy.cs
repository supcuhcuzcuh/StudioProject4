using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseEnemy :  Entity
{
    public PlayerStats enemyStats;
    public Weapon enemyWeapon;
    public Rigidbody rb;
    public Animator enemyAnimator;

    [Header("Base Enemy Variables")]
    //public float health;
    public float hitsToDie;
}
