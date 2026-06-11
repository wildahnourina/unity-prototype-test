using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    public static Player instance;
    public PlayerInputSet input { get; private set; }
    public UI ui { get; private set; }

    #region Player States
    public Player_IdleState idleState { get; private set; }
    public Player_WalkState walkState { get; private set; }
    //public Player_RunState runState { get; private set; }
    //public Player_CaughtState caughtState { get; private set; }
    //public Player_RespawnState respawnState { get; private set; }
    #endregion

    public FlashlightController flashlight { get; private set; }
    private Object_Interactable lastPrompted;

    public Vector2 moveInput { get; private set; }
    private Vector2 lastMoveInput;

    [Header("Movement Details")]
    public float moveSpeed;
    public float jumpForce = 5;
    [Range(0, 1)]
    public float inAirMultiplier = .4f;

    private IInteractable currentInteractable;

    protected override void Awake()
    {
        base.Awake();
        instance = this;

        input = new PlayerInputSet();
        ui = FindAnyObjectByType<UI>();
        flashlight = GetComponentInChildren<FlashlightController>(true);

        ui.SetupControlsUI(input);

        idleState = new Player_IdleState(this, stateMachine, "idle");
        walkState = new Player_WalkState(this, stateMachine, "move");
        //runState = new Player_RunState(this, stateMachine, "run");
        //caughtState = new Player_CaughtState(this, stateMachine, "morningstar pose");
        //respawnState = new Player_RespawnState(this, stateMachine, "crouch");
    }

    protected override void Start()
    {
        base.Start();

        //if (GameManager.instance.isRespawning)
        //{
        //    GameManager.instance.isRespawning = false;
        //    stateMachine.Initialize(respawnState);
        //}
        //else
            stateMachine.Initialize(idleState);
    }


    protected override void Update()
    {
        base.Update();
        UpdatePrompt();

        HandleDirectionalInteract();
    }
    
    private void TryInteract()
    {
        //IInteractable closest = GetClosestInteractable();

        //if (closest == null)
        //    return;

        currentInteractable?.Interact(this);
    }

    private void TryEnterArea(Vector2 dir)
    {
        IInteractable closest = GetClosestInteractable();

        if (closest == null)
            return;

        closest.Interact(dir);
    }

    void HandleDirectionalInteract()
    {
        if (moveInput == Vector2.zero)
        {
            lastMoveInput = Vector2.zero;
            return;
        }

        // cuma trigger saat pertama kali pencet arah
        if (lastMoveInput == Vector2.zero)
        {
            TryEnterArea(moveInput);
        }

        lastMoveInput = moveInput;
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

            float distanceX = Mathf.Abs(transform.position.x - target.transform.position.x);

            if (distanceX < closestDistance)
            {
                closestDistance = distanceX;
                closest = interactable;
            }            
        }
        return closest;
    }

    private void UpdatePrompt()
    {
        //IInteractable closest = GetClosestInteractable();
        //Object_Interactable current = closest as Object_Interactable;

        currentInteractable = GetClosestInteractable();
        Object_Interactable current = currentInteractable as Object_Interactable;


        if (current == lastPrompted)
            return;

        lastPrompted?.HidePrompt();
        lastPrompted = current;
        lastPrompted?.ShowPrompt();
    }

    public void OnCaught()
    {
        //stateMachine.ChangeState(caughtState);
        GameManager.instance.isRespawning = true;
        GameManager.instance.ChangeScene(SceneManager.GetActiveScene().name, 1.2f);
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
