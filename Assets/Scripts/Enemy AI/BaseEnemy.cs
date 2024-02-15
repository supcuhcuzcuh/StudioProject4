using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
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
