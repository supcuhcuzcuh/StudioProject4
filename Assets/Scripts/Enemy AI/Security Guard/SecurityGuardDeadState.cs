using UnityEngine;

public class SecurityGuardDeadState : State
{
    [SerializeField] private WaypointsTracker destinationTracker;
    public override State PlayCurrentState()
    {
        destinationTracker.agent.ResetPath();
        enemy.enemyWeapon.SetWeapon();

        enemy.enemyAnimator.SetTrigger("isDead");
        return this;
    }
}
    