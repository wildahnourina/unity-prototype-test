using UnityEngine;

public class Player_RunState : Player_GroundedState
{
    public Player_RunState(Player player, StateMachine stateMachine, SpineAnimator anim, string animName) : base(player, stateMachine, anim, animName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput.x == 0)
            stateMachine.ChangeState(player.idleState);

        player.SetVelocity(player.moveInput.x * player.moveSpeed * 2, rb.linearVelocity.y);

        if (!input.Player.Run.IsPressed())
        {
            stateMachine.ChangeState(player.walkState);
            return;
        }
    }
}
