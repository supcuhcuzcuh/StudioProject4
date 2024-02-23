using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitscanWeapon : Weapon
{
    [SerializeField]
    protected float weaponRange;
    [SerializeField]
    protected float hitForce;

    [SerializeField] protected LayerMask layermask;
    public Transform dirShoot;

    //[SerializeField] protected Vector3 startFrom;

    protected void DoHitscan()
    {
      
        RaycastHit hit;
       
        if (Physics.Raycast(dirShoot.position, dirShoot.forward, out hit, weaponRange, layermask))
        {
            Debug.Log(hit.collider.gameObject);
            HitscanHit(hit);
        }
        else
        {
            HitscanMiss(muzzlePosition.transform.position);
        }
    }

    protected abstract void HitscanHit(RaycastHit _hit);

    protected abstract void HitscanMiss(Vector3 _rayOrigin);

}
