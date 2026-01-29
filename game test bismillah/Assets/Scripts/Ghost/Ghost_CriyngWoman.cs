using UnityEngine;

public class Ghost_CriyngWoman : Ghost
{
    protected override void Awake()
    {
        base.Awake();

        idleState = new Ghost_IdleState(this, stateMachine, anim, "idle");
        activeState = new Ghost_ActiveState(this, stateMachine, anim, "head-turn");
        chaseState = new Ghost_ChaseState(this, stateMachine, anim, "run");
        caughtState = new Ghost_CaughtState(this, stateMachine, anim, "attack");
    }
}
