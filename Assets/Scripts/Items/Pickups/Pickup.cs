using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    void OnTriggerEnter(Collider _col)
    {
        PickupEffect(_col);
        
    }

    public virtual void PickupEffect(Collider _col)
    {

    }
}
