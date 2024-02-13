using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField]
    private int ammoAmount;

    public override void PickupEffect(Collider _col)
    {
            _col.GetComponent<GunController>().playerStats.playerAmmo += ammoAmount;
            GameObject.Find("PlayerStatsUI").GetComponent<PlayerStatsUIManager>().UpdateAmmoUI();
            Destroy(gameObject);
    }
}
