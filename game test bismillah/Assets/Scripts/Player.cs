using System;
using UnityEngine;

public class Player : Entity
{
    public PlayerInputSet input { get; private set; }
    public UI ui { get; private set; }
    public Player_IdleState idleState { get; private set; }
    public Player_WalkState walkState { get; private set; }
    public Player_RunState runState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }

    public FlashlightController flashlight { get; private set; }
    private Object_Interactable lastPrompted;

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
        ui = FindAnyObjectByType<UI>();
        flashlight = GetComponentInChildren<FlashlightController>();

        ui.SetupControlsUI(input);

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


    protected override void Update()
    {
        base.Update();
        UpdatePrompt();
    }

    private void TryInteract()
    {
        IInteractable closest = GetClosestInteractable();

        if (closest == null)
            return;

        closest.Interact();
    }

    public IInteractable GetClosestInteractable()
    {
        float closestDistance = Mathf.Infinity;
        IInteractable closest = null;

        Collider2D[] objectsAround = Physics2D.OverlapCircleAll(transform.position, 1f);

        foreach (var target in objectsAround)
        {
            IInteractable interactable = target.GetComponent<IInteractable>();
            if (interactable == null) continue;

            float distance = Vector2.Distance(transform.position, target.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = interactable;
            }
        }
        return closest;
    }

    private void UpdatePrompt()
    {
        IInteractable closest = GetClosestInteractable();

        Object_Interactable current = closest as Object_Interactable;

        if (current == lastPrompted)
            return;

        lastPrompted?.HidePrompt();
        lastPrompted = current;
        lastPrompted?.ShowPrompt();
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

        input.Player.Interact.performed += ctx => TryInteract();
        input.Player.Flashlight.performed += ctx => flashlight.Toggle();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
