using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField]
    protected float health;

    [SerializeField]
    protected float maxHealth = 100;

    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float _health)
    {
        health = _health;
    }

    public void AddHealth(float _health)
    {
        if(health + _health > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += _health;
        }
    }

    public virtual void OnDamaged(float damage)
    {
        health -= damage;
    }
}
