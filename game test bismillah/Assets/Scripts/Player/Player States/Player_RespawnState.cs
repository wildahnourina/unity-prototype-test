using UnityEngine;

public class Player_RespawnState : Player_GroundedState
{
    private float timer;
    private const float wakeTime = 1.5f;

    public Player_RespawnState(Player player, StateMachine stateMachine, SpineAnimator anim, string animName) : base(player, stateMachine, anim, animName)
    {
        loopAnim = false;
    }

    public override void Enter()
    {
        base.Enter();

        timer = 0f;
        input.Disable();
    }

    public override void Update()
    {
        base.Update();

        timer += Time.deltaTime;

        if (timer >= wakeTime)
        {
            input.Enable();
            stateMachine.ChangeState(player.idleState);
        }
    }
}
