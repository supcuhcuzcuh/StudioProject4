using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorialToolTip : ScriptedEvent
{
    [SerializeField]
    protected string toolTipText;

    [SerializeField]
    protected ToolTipManager tooltipManager;

    protected override void TriggerScriptedEvent(Collider col)
    {
        tooltipManager.ShowToolTip(toolTipText);
    }

}
