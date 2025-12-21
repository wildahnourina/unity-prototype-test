using System;
using System.Collections;
using UnityEngine;
using Spine.Unity;

public class Player : Entity
{
    public PlayerInputSet input { get; private set; }

    public Player_IdleState idleState { get; private set; }
    public Player_WalkState walkState { get; private set; }
    public Player_RunState runState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }

    public Vector2 moveInput { get; private set; }
    [Header("Movement Details")]
    public float moveSpeed;
    public float jumpForce = 5;
    [Range(0, 1)]
    public float inAirMultiplier = .4f;

    protected override void Awake()
    {
        base.Awake();
        input = new PlayerInputSet();

        idleState = new Player_IdleState(this, stateMachine, anim, "idle");
        walkState = new Player_WalkState(this, stateMachine, anim, "walk");
        runState = new Player_RunState(this, stateMachine, anim, "run");
        jumpState = new Player_JumpState(this, stateMachine, anim, "jump");
        fallState = new Player_FallState(this, stateMachine, anim, "fall");
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
