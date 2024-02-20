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

        // Get the relative rotation to the target rotation
        Quaternion relativeRotation = targetRotation * Quaternion.Inverse(turretBarrel.rotation);

        // Separate the relative rotation into its Euler angles
        Vector3 relativeEulerAngles = relativeRotation.eulerAngles;

        // Calculate the clamped rotation on the x-axis (tilt)
        float clampedXRotation = Mathf.Clamp(relativeEulerAngles.x, -maxDownRotation, maxUpRotation);

        // Create a new quaternion with the clamped x-axis rotation and original y and z rotations
        Quaternion clampedRotation = Quaternion.Euler(clampedXRotation, relativeEulerAngles.y, relativeEulerAngles.z);

        // Apply the smoothed rotation to the turret barrel
        turretBarrel.rotation = Quaternion.Slerp(turretBarrel.rotation, targetRotation * clampedRotation, turretRotationSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
       // Instantiate the projectile at the boss's position
    GameObject projectile = Instantiate(projectilePrefab, Firepos.position, turretBarrel.rotation);

    // Get the rigidbody component of the projectile
    Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();

    // Apply impulse force in the forward direction of the turret barrel
    float projectileSpeed = 10.0f; // You can adjust the speed as needed
    projectileRigidbody.AddForce(turretBarrel.forward * projectileSpeed, ForceMode.Impulse);

    // Reset the cooldown timer
    timeSinceLastShot = 0.0f;
    }
}
