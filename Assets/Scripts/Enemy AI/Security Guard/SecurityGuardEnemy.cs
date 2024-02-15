using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuardEnemy : BaseEnemy
{
    void TakeHit()
    {
        hitsToDie -= 1;
        if (hitsToDie <= 0)
        {
            Debug.Log("ENEMY IS DEAD");
        }
    }
}
