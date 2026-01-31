using UnityEngine;

public class Ghost_WalkBackState : GhostState
{
    public Ghost_WalkBackState(Ghost ghost, StateMachine stateMachine, SpineAnimator anim, string animName) : base(ghost, stateMachine, anim, animName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (ghost.PlayerDetected())
            stateMachine.ChangeState(ghost.chaseState);

        ghost.SetVelocity(ghost.GetWalkMoveSpeed() * DirectionToIdle(), rb.linearVelocity.y);

        if (ReachedIdle())
        {
            ghost.ClearTrigger();
            stateMachine.ChangeState(ghost.idleState);
        }
    }

    private int DirectionToIdle()
    {
        Vector2 idlePos = ghost.idlePosition;

        if (Mathf.Abs(idlePos.x - ghost.transform.position.x) < 0.05f)
            return 0;

        return idlePos.x > ghost.transform.position.x ? 1 : -1;
    }

    private bool ReachedIdle()
    {
        return Mathf.Abs(ghost.transform.position.x - ghost.idlePosition.x) < 0.05f;
    }
}
