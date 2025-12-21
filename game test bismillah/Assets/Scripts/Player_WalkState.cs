using UnityEngine;

public class Player_WalkState : Player_GroundedState
{
    public Player_WalkState(Player player, StateMachine stateMachine, SpineAnimator anim, string animName) : base(player, stateMachine, anim, animName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput.x == 0)
            stateMachine.ChangeState(player.idleState);

        player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.linearVelocity.y);

        if (input.Player.Run.IsPressed())
        {
            stateMachine.ChangeState(player.runState);
            return;
        }
    }
}
