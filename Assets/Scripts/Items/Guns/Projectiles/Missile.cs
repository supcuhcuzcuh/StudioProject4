using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

 // projectile revised to use UnityEngine.Pool in Unity 2021
    public class Missile : RevisedProjectile
    {

        [SerializeField]
        private ParticleEffectManager particleEffectManager;

        public float radius = 10f;
        public float force = 300f;

        void OnEnable()
        {
            //particleEffectManager.PlayParticleEffect("BazookaRocketTrail", transform.position, transform);
        }

        void  OnCollisionEnter(Collision _col)
        {                       
            ProjectileCollide(_col.gameObject);

            particleEffectManager.PlayParticleEffect("Explosion", transform.position);
            GameObject.Find("CameraHolder").GetComponent<PerlinNoiseShake>().QueueExplosion(7, 1);
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider nearbyObject in colliders)
            {
                if (nearbyObject.gameObject.GetComponent<DesctrutableObjects>())
                {
                    nearbyObject.gameObject.GetComponent<DesctrutableObjects>().OnDamaged(weaponSource.weaponDamage);
                }

                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                }

            }
        }
    }
