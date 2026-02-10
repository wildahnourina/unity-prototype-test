using System;
using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public static Player instance;
    public PlayerInputSet input { get; private set; }
    public UI ui { get; private set; }

    #region Player States
    public Player_IdleState idleState { get; private set; }
    public Player_WalkState walkState { get; private set; }
    public Player_RunState runState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_CaughtState caughtState { get; private set; }
    public Player_RespawnState respawnState { get; private set; }
    #endregion

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
        instance = this;

        input = new PlayerInputSet();
        ui = FindAnyObjectByType<UI>();
        flashlight = GetComponentInChildren<FlashlightController>(true);

        ui.SetupControlsUI(input);

        idleState = new Player_IdleState(this, stateMachine, anim, "idle");
        walkState = new Player_WalkState(this, stateMachine, anim, "walk");
        runState = new Player_RunState(this, stateMachine, anim, "run");
        jumpState = new Player_JumpState(this, stateMachine, anim, "jump");
        fallState = new Player_FallState(this, stateMachine, anim, "fall");
        caughtState = new Player_CaughtState(this, stateMachine, anim, "morningstar pose");
        respawnState = new Player_RespawnState(this, stateMachine, anim, "crouch");
    }

    protected override void Start()
    {
        base.Start();

        if (GameManager.instance.isRespawning)
            stateMachine.Initialize(respawnState);
        else
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

    public void OnCaught()
    {
        stateMachine.ChangeState(caughtState);
        GameManager.instance.RestartGame();
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

        input.Player.Interact.performed += ctx => TryInteract();
        input.Player.Flashlight.performed += ctx =>
        {
            if (flashlight.gameObject.activeInHierarchy)
                flashlight.Toggle();
        };
    }

    private void OnDisable() => input.Disable();


}
