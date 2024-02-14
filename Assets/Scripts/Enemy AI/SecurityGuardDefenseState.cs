using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuardDefenseState : State
{
    [Header("Potential States Of Transition")]
    public SecurityGuardAlertState alertState;
    public SecurityGuardDeadState deadState;
    public bool isDead;
    public override State PlayCurrentState()
    {
        if (isDead)
        {
            return deadState;
        }
        else
        {
            return this;
        }
    }
}
