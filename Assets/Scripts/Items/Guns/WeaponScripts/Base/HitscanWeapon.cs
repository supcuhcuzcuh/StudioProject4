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

    private void Start()
    {
        layermask = ~0;         //set layermask to all layers
        layermask |= (1 << 7);    //remove player layer
        layermask |= (1 << 8);    //remove pickup layer

        Init();
    }

    protected virtual void Init()
    {

    }

    protected void DoHitscan()
    {
        Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        RaycastHit hit;      

        if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out hit, weaponRange, layermask))
        {
            HitscanHit(hit);
        }
        else
        {
            HitscanMiss(rayOrigin);
        }
    }

    protected abstract void HitscanHit(RaycastHit _hit);

    protected abstract void HitscanMiss(Vector3 _rayOrigin);

}
