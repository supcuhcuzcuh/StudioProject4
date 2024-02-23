using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelTutorialEvent : ScriptedEvent
{
    [SerializeField]
    protected string toolTipText;

    [SerializeField]
    protected ToolTipManager tooltipManager;

    [SerializeField]
    protected PlayerStats playerStats;

    [SerializeField]
    protected GameObject remoteOnFloor;

    protected override void TriggerScriptedEvent(Collider col)
    {
        tooltipManager.ShowToolTip(toolTipText);
        playerStats.canTimeTravel = true;
        remoteOnFloor.SetActive(false);
    }
}
