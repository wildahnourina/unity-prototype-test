using UnityEngine;
using UnityEngine.Windows;

public class Player : Entity
{
    public Player_IdleState idleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new Player_IdleState(this, stateMachine, anim, "idle");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
}
