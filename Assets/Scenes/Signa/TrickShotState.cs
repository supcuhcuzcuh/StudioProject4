using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrickShotState : State
{
    // Reference to the player's transform
    public Transform playerTransform;

    // Projectile prefab to instantiate
    public GameObject projectilePrefab;

    // Reference to the turret barrel transform
    public Transform turretBarrel;

    public Transform firePos;

    public float rotationSpeed = 180f; // Adjust the rotation speed as needed

    private float totalRotation = 0f;
    private bool hasRotatedOnce = false;

    private float delayTimer = 3f;
    private bool hasStartedRotation = false;

    [SerializeField] private GameObject laserbeam;

    [SerializeField] private GameObject Theparticle;

    public override State PlayCurrentState()
    {
        if (!hasStartedRotation)
        {
            Theparticle.SetActive(true);
            delayTimer -= Time.deltaTime;

            if (delayTimer <= 0f)
            {
                hasStartedRotation = true;
                delayTimer = 0f; // Reset the timer for future use if needed
            }
        }
        else if (!hasRotatedOnce)

        {
            laserbeam.SetActive(true);
            Theparticle.SetActive(false);
            turretBarrel.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            totalRotation += rotationSpeed * Time.deltaTime;

            if (totalRotation >= 720f)
            {
                hasRotatedOnce = true;
                totalRotation = 0f; // Reset totalRotation for future use if needed
            }
        }

        return this;
    }

}
