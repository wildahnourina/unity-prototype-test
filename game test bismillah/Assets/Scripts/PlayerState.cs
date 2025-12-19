using UnityEngine;

public class PlayerState : EntityState
{
    protected Player player;

    public PlayerState(Player player, StateMachine stateMachine, SpineAnimator anim, string animName) : base(stateMachine, anim, animName)
    {
        this.player = player;
        rb = player.rb;
    }
}
