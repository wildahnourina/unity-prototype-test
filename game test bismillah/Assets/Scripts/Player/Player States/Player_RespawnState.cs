using UnityEngine;

public class Player_RespawnState : Player_GroundedState
{
    public Player_RespawnState(Player player, StateMachine stateMachine, SpineAnimator anim, string animName) : base(player, stateMachine, anim, animName)
    {
    }
}
