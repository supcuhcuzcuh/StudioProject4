using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public abstract class BaseEnemy : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] protected Animator enemyAnimator;

    protected float health;
    protected float hitsToDie;
    private void Start()
    {
        if (enemyAnimator != null)
        {
            enemyAnimator = GetComponent<Animator>();
        }
    }
}
