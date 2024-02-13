using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

 // projectile revised to use UnityEngine.Pool in Unity 2021
    public class RevisedProjectile : MonoBehaviour
    {
        // deactivate after delay
        [SerializeField] private float timeoutDelay = 3f;

        private IObjectPool<RevisedProjectile> objectPool;

        // public property to give the projectile a reference to its ObjectPool
        public IObjectPool<RevisedProjectile> ObjectPool { set => objectPool = value; }

        public ProjectileWeapon weaponSource;

        public void Deactivate()
        {
            StartCoroutine(DeactivateRoutine(timeoutDelay));
        }

        IEnumerator DeactivateRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);

            // reset the moving Rigidbody
            Rigidbody rBody = GetComponent<Rigidbody>();
            rBody.velocity = new Vector3(0f, 0f, 0f);
            rBody.angularVelocity = new Vector3(0f, 0f, 0f);

            // release the projectile back to the pool
            objectPool.Release(this);
        }
    
        public void ProjectileCollide(GameObject _hitObject)
        {
            if (_hitObject.tag != "Player")
            {              
                // reset the moving Rigidbody
                Rigidbody rBody = GetComponent<Rigidbody>();
                rBody.velocity = new Vector3(0f, 0f, 0f);
                rBody.angularVelocity = new Vector3(0f, 0f, 0f);

                // release the projectile back to the pool
                objectPool.Release(this);
            }
        }

       
}

