using UnityEngine;
using System.Collections;
using Spine;

public class Ghost_ActiveState : GhostState
{
    public Ghost_ActiveState(Ghost ghost, StateMachine stateMachine, SpineAnimator anim, string animName) : base(ghost, stateMachine, anim, animName)
    {
        loopAnim = false;
    }

    public override void Enter()
    {
        base.Enter();
        if (entry != null)
            entry.Complete += OnComplete;
    }

    public override void Exit()
    {
        base.Exit();
        if (entry != null)
            entry.Complete -= OnComplete;
    }


    private void OnComplete(TrackEntry _)
    {
        if (ghost.PlayerDetected())
        {
            ghost.HandleFlip(DirectionToPlayer());
            stateMachine.ChangeState(ghost.chaseState);
            return;
        }

        ghost.ClearTrigger();
        stateMachine.ChangeState(ghost.idleState);
    }

    protected int DirectionToPlayer()
    {
        if (player == null)
            return 0;

        return player.position.x > ghost.transform.position.x ? 1 : -1;
    }
}
