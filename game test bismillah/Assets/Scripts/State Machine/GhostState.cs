using UnityEngine;

public class GhostState : EntityState
{
    protected Ghost ghost;
    public GhostState(Ghost ghost, StateMachine stateMachine, SpineAnimator anim, string animName) : base(stateMachine, anim, animName)
    {
        this.ghost = ghost;
        rb = ghost.rb;
    }
}
