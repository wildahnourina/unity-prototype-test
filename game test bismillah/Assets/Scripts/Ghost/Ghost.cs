using UnityEngine;
using System.Collections;

public class Ghost : Entity
{
    [Header("Chase Details")]
    public float chaseSpeed = 1.4f;
    public float caughtDistance;

    [Header("Player Detection")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance = 10;
    public Transform player { get; private set; }

    public Ghost_IdleState idleState;
    public Ghost_ActiveState activeState;
    public Ghost_ChaseState chaseState;
    public Ghost_CaughtState caughtState;

    //[HideInInspector]
    public bool isTriggered;
    //public bool canActive;
    //public bool hasEverActive = false;//sudah pernah active

    public float GetChaseMoveSpeed() => chaseSpeed;


    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    public void Trigger()
    {
        if (stateMachine.currentState != idleState)
            return;

        if (isTriggered) return;

        isTriggered = true;

        //if (!hasEverActive)//ketika trigger pertama kali
        //    canActive = true;
    }

    public void ClearTrigger() => isTriggered = false;

    public Transform GetPlayerReference()
    {
        //if (player != null)
        //    return player;

        //RaycastHit2D hit = PlayerDetected();

        //if (!hit)
        //    return null;

        //player = hit.transform;

        if (player == null)
            player = PlayerDetected().transform;
        return player;
    }

    
    public RaycastHit2D PlayerDetected()
    {
        Vector2 origin = playerCheck.position;

        Vector2 dirFront = Vector2.right * facingDirection;
        Vector2 dirBack = -dirFront;

        RaycastHit2D hitFront = Physics2D.Raycast(origin, dirFront, playerCheckDistance, whatIsPlayer | whatIsGround);
        RaycastHit2D hitBack = Physics2D.Raycast(origin, dirBack, playerCheckDistance, whatIsPlayer | whatIsGround);

        if (IsPlayer(hitFront)) return hitFront;
        if (IsPlayer(hitBack)) return hitBack;

        return default;
    }

    private bool IsPlayer(RaycastHit2D hit)
    {
        if (hit.collider == null)
            return false;

        return hit.collider.gameObject.layer == LayerMask.NameToLayer("Player");
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDirection * playerCheckDistance), playerCheck.position.y));        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDirection * caughtDistance), playerCheck.position.y));
    }
}
