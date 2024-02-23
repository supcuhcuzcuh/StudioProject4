using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorialToolTip : ScriptedEvent
{
    [SerializeField]
    private string toolTipText;

    [SerializeField] 
    private ToolTipManager tooltipManager;


    protected override void TriggerScriptedEvent(Collider col)
    {
        tooltipManager.ShowToolTip(toolTipText);
    }
}
