using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootState : State
{
    // Reference to the player's transform
    public Transform playerTransform;

    // Projectile prefab to instantiate
    public GameObject projectilePrefab;

    // Shooting cooldown to control the rate of fire
    public float shootCooldown = 1.0f;
    private float timeSinceLastShot = 0.0f;

    // Turret rotation speed
    public float turretRotationSpeed = 5.0f;

    // Limit for up and down rotation
    public float maxUpRotation = 50.0f;
    public float maxDownRotation = 50.0f;

    // Reference to the turret barrel transform
    public Transform turretBarrel;


    public Transform Firepos;



    //public BossShooting shootin;
    public override State PlayCurrentState()
    {
        // Calculate direction to the player
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

        RotateTurretBarrel(directionToPlayer);
        // Check if enough time has passed since the last shot
        if (timeSinceLastShot >= shootCooldown)
        {
            // Shoot towards the player
            Shoot();
        }

        // Update the cooldown timer
        timeSinceLastShot += Time.deltaTime;

        return this;
    }

    private void RotateTurretBarrel(Vector3 targetDirection)
    {
        // Calculate the rotation needed to look at the player
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // Smoothly rotate the turret barrel towards the player using lerp
        turretBarrel.rotation = Quaternion.Lerp(turretBarrel.rotation, targetRotation, turretRotationSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        // Instantiate the projectile at the boss's position
        GameObject projectile = Instantiate(projectilePrefab, Firepos.position, Quaternion.identity);

        // Get the rigidbody component of the projectile
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();

        // Calculate direction to the player
        Vector3 shootDirection = (playerTransform.position - transform.position).normalized;

        // Apply impulse force towards the player
        float projectileSpeed = 10.0f; // You can adjust the speed as needed
        projectileRigidbody.AddForce(shootDirection * projectileSpeed, ForceMode.Impulse);

        //shootin.FireWeapon();
        // Reset the cooldown timer
        timeSinceLastShot = 0.0f;
    }
}
