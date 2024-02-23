using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void Start()
    {
        // Destroy the missile after exploding
        Destroy(gameObject,5.0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player or any other object you want to damage
        if (other.CompareTag("Player"))
        {
            // Apply damage to the player
            FPSControls playerControls = other.GetComponent<FPSControls>();
            if (playerControls != null)
            {
                playerControls.OnDamaged(10);
                Destroy(gameObject);
            }
        }

    
    }
}
