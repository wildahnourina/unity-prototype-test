using System;
using System.Collections;
using UnityEngine;
using Spine.Unity;

public class Player : Entity
{
    public PlayerInputSet input { get; private set; }

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }

    public Vector2 moveInput { get; private set; }
    [Header("Movement Details")]
    public float moveSpeed;
    public float jumpForce = 5;

    protected override void Awake()
    {
        base.Awake();
        input = new PlayerInputSet();

        idleState = new Player_IdleState(this, stateMachine, anim, "idle");
        moveState = new Player_MoveState(this, stateMachine, anim, "walk");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
