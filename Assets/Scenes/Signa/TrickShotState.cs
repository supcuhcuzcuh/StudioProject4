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

    public override State PlayCurrentState()
    {
      
        return this;
    }

   
}
