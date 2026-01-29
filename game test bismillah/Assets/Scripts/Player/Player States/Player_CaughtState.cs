using UnityEngine;

public class Player_CaughtState : Player_GroundedState
{
    public Player_CaughtState(Player player, StateMachine stateMachine, SpineAnimator anim, string animName) : base(player, stateMachine, anim, animName)
    {
        loopAnim = false;
    }

    public override void Enter()
    {
        base.Enter();

        input.Disable();
        rb.simulated = false;
        Debug.Log("Player ketangkep");
    }
}
