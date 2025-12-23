using UnityEngine;
using UnityEngine.Windows;

public class PlayerState : EntityState
{
    protected Player player;
    protected PlayerInputSet input;

    public PlayerState(Player player, StateMachine stateMachine, SpineAnimator anim, string animName) : base(stateMachine, anim, animName)
    {
        this.player = player;
        rb = player.rb;
        input = player.input;
    }
}
