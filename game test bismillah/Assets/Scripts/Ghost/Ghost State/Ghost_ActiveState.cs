using UnityEngine;

public class Ghost_ActiveState : GhostState
{
    public Ghost_ActiveState(Ghost ghost, StateMachine stateMachine, SpineAnimator anim, string animName) : base(ghost, stateMachine, anim, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //ghost.ClearTrigger();
    }
}
