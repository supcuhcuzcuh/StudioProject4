using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

// projectile revised to use UnityEngine.Pool in Unity 2021
public class HomingMissile : RevisedProjectile
{

    [SerializeField]
    private ParticleEffectManager particleEffectManager;

    public float radius = 10f;
    public float force = 300f;

    public Transform missileTarget;

    void OnEnable()
    {
        //particleEffectManager.PlayParticleEffect("BazookaRocketTrail", transform.position, transform);
        if(weaponSource != null)
        {
            missileTarget = weaponSource.GetTarget();
        }
       
    }

    void Start()
    {
        //particleEffectManager.PlayParticleEffect("BazookaRocketTrail", transform.position, transform);
        missileTarget = weaponSource.GetTarget();
    }

    void Update()
    {
        Vector3 dir = (missileTarget.position - transform.position).normalized;
        transform.position += dir * Time.deltaTime * 10f;
        transform.forward = dir;

    }

    void OnCollisionEnter(Collision _col)
    {
        ProjectileCollide(_col.gameObject);

        particleEffectManager.PlayParticleEffect("Explosion", transform.position);
        GameObject.Find("CameraHolder").GetComponent<PerlinNoiseShake>().QueueExplosion(7, 1);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.gameObject.GetComponent<Entity>())
            {
                nearbyObject.gameObject.GetComponent<Entity>().OnDamaged(weaponSource.weaponDamage);
            }

            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }

        }
    }
}

