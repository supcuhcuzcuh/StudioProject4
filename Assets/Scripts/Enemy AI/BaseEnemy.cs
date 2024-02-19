using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public abstract class BaseEnemy : MonoBehaviour
{
    public Weapon enemyWeapon;
    public Rigidbody rb;
    public Animator enemyAnimator;

    protected float health;
    protected float hitsToDie;
}
