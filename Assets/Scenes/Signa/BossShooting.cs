using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class BossShooting : ProjectileWeapon
{
    public Transform Firepos;

    // Reference to the player's transform
    public Transform playerTransform;
    public override void FireWeapon()
    {

        if (objectPool != null)
        {
            RevisedProjectile bulletObject = objectPool.Get();

            if (bulletObject == null)
                return;

            bulletObject.weaponSource = this;

            // Get the rigidbody component of the projectile
            Rigidbody projectileRigidbody = bulletObject.GetComponent<Rigidbody>();

            // Calculate direction to the player
            Vector3 shootDirection = (playerTransform.position - transform.position).normalized;

            // Apply impulse force towards the player
            float projectileSpeed = 50.0f; // You can adjust the speed as needed
            projectileRigidbody.AddForce(shootDirection * projectileSpeed, ForceMode.Impulse);
        }

    }


}
