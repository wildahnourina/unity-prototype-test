using UnityEngine;

public class Ghost_IdleState : GhostState
{
    public Ghost_IdleState(Ghost ghost, StateMachine stateMachine, SpineAnimator anim, string animName) : base(ghost, stateMachine, anim, animName)
    {
    }

    //public override void Enter()
    //{
    //    base.Enter();

    //    //ghost.ClearTrigger();
    //}

    public override void Update()
    {
        base.Update();

        if (ghost.isTriggered && ghost.PlayerDetected())
        {
            stateMachine.ChangeState(ghost.activeState);
        }
    }
}
