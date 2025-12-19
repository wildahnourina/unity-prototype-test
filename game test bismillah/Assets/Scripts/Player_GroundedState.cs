using UnityEngine;

public class Player_GroundedState : PlayerState
{
    public Player_GroundedState(Player player, StateMachine stateMachine, SpineAnimator anim, string animName) : base(player, stateMachine, anim, animName)
    {
    }
}
