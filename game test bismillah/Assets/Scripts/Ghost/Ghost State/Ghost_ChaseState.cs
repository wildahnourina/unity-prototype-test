using UnityEngine;

public class Ghost_ChaseState : GhostState
{
    public Ghost_ChaseState(Ghost ghost, StateMachine stateMachine, SpineAnimator anim, string animName) : base(ghost, stateMachine, anim, animName)
    {

    }

    public override void Update()
    {
        base.Update();

        if (!ghost.PlayerDetected())
        {
            ghost.ClearTrigger();
            stateMachine.ChangeState(ghost.idleState);
        }

        if (InCaughtRange() && ghost.PlayerDetected())
            stateMachine.ChangeState(ghost.caughtState);
        else
            ghost.SetVelocity(ghost.GetChaseMoveSpeed() * DirectionToPlayer(), rb.linearVelocity.y);
    }

    protected bool InCaughtRange() => DistanceToPlayer() < ghost.caughtDistance;

    protected float DistanceToPlayer()
    {
        if (player == null)
            return float.MaxValue;

        return Mathf.Abs(player.position.x - ghost.transform.position.x);
    }

    protected int DirectionToPlayer()
    {
        if (player == null)
            return 0;

        return player.position.x > ghost.transform.position.x ? 1 : -1;
    }
}
