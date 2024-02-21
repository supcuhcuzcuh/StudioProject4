using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenLever : Lever
{
    override public void OnActivate()
    {
        GameObject.Find("ToolTip").GetComponent<ToolTipManager>().ShowToolTip("The lever is jammed. Looks like I'll have to force it open..");
    }
}
