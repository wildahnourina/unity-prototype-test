using UnityEngine;

public class Ghost : Entity
{
    public Ghost_IdleState idleState { get; private set; }
    //public Ghost_ActiveState activeState { get; private set; }
    //public Ghost_ChaseState chaseState { get; private set; }
    //public Ghost_CaughtState caughtState { get; private set; }

    public bool isTriggered;

    protected override void Awake()
    {
        base.Awake();

        idleState = new Ghost_IdleState(this, stateMachine, anim, "idle");
        //activeState = new Ghost_ActiveState(this, stateMachine, anim, "idle");
        //chaseState = new Ghost_ChaseState(this, stateMachine, anim, "idle");
        //caughtState = new Ghost_CaughtState(this, stateMachine, anim, "idle");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    public void Trigger() => isTriggered = true;
    public void ClearTrigger() => isTriggered = false;
}
