using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeteriaFightEvent : ScriptedEvent
{
    [SerializeField]
    private PlayerRespawn playerRespawn;

    protected override void TriggerScriptedEvent(Collider col)
    {
        playerRespawn.SetNextSpawnPoint();
        //Spawn Enemies
    }
}
