using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField]
    private int healthAmount;

    public override void PickupEffect(Collider _col)
    {
  
        _col.GetComponent<FPSControls>().AddHealth(healthAmount);
        GameObject.Find("PlayerStatsUI").GetComponent<PlayerStatsUIManager>().UpdateHealthUI(_col.GetComponent<FPSControls>().GetHealth().ToString());
        Destroy(gameObject);
    }
}
