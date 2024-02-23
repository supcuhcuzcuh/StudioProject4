using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunTutorialEvent : ScriptedEvent
{
    [SerializeField]
    private PlayerRespawn playerRespawn;

    protected override void TriggerScriptedEvent(Collider col)
    {
        playerRespawn.SetNextSpawnPoint();
        //Show ToolTip
    }
}
