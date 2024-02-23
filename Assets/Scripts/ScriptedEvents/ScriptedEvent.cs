using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptedEvent : MonoBehaviour
{
    protected abstract void TriggerScriptedEvent(Collider col);
    protected bool triggered = false;

    void OnTriggerEnter(Collider col)
    {
        if(triggered == false)
        {
            TriggerScriptedEvent(col);
            triggered = true;
        }
        
    }

}
