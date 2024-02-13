using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOnGround : Pickup
{
    [SerializeField]
    private int fireDamage;

    [SerializeField]
    private ParticleEffectManager particleEffectManager;

    public override void PickupEffect(Collider _col)
    {
        Debug.Log(_col.gameObject);     
        _col.gameObject.GetComponent<Entity>().OnDamaged(fireDamage);
        particleEffectManager.PlayParticleEffect("FireOnObject", _col.gameObject.transform.position, _col.gameObject.transform);
        GameObject.Find("PlayerStatsUI").GetComponent<PlayerStatsUIManager>().UpdateHealthUI(_col.GetComponent<FPSControls>().GetHealth().ToString());
    }
}
