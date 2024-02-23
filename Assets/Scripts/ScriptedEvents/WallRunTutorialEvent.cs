using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunTutorialEvent : ShowTutorialToolTip
{
    [SerializeField]
    private PlayerRespawn playerRespawn;

    protected override void TriggerScriptedEvent(Collider col)
    {
        tooltipManager.ShowToolTip(toolTipText);
        playerRespawn.SetNextSpawnPoint();
        //Show ToolTip
    }
}
