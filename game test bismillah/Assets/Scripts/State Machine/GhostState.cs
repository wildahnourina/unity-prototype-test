using UnityEngine;

public class GhostState : EntityState
{
    protected Ghost ghost;
    protected Transform player;

    public GhostState(Ghost ghost, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.ghost = ghost;

        anim = ghost.anim;
        rb = ghost.rb;
    }

    //public GhostState(Ghost ghost, StateMachine stateMachine, SpineAnimator anim, string animName) : base(stateMachine, anim, animName)
    //{
    //    this.ghost = ghost;
    //    rb = ghost.rb;
    //}

    public override void Enter()
    {
        base.Enter();

        if (player == null)
            player = ghost.GetPlayerReference();
    }
}
