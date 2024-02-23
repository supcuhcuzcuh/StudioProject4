using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPit : ScriptedEvent
{
    protected override void TriggerScriptedEvent(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<FPSControls>().OnDamaged(999);
        }
    }
}
