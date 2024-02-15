using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuardDeadState : State
{
    public override State PlayCurrentState()
    {
        Debug.Log("PLAYING DEAD STATE");
        return this;
    }
}
