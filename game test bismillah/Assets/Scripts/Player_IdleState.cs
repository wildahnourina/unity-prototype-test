using UnityEngine;

public class Player_IdleState : Player_GroundedState
{
    public Player_IdleState(Player player, StateMachine stateMachine, SpineAnimator anim, string animName) : base(player, stateMachine, anim, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, rb.linearVelocity.y);
    }
}
