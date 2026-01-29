using Spine;
using UnityEngine;

public class Ghost_CaughtState : GhostState
{
    public Ghost_CaughtState(Ghost ghost, StateMachine stateMachine, SpineAnimator anim, string animName) : base(ghost, stateMachine, anim, animName)
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
        ghost.ClearTrigger();
        stateMachine.ChangeState(ghost.idleState);

        player.GetComponent<Player>().OnCaught();
    } 
}