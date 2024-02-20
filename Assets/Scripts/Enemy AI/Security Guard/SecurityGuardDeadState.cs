using UnityEngine;

public class SecurityGuardDeadState : State
{
    public override State PlayCurrentState()
    {
        transform.root.GetComponent<BaseEnemy>().enemyAnimator.SetTrigger("isDead");
        return this;
    }
}
    