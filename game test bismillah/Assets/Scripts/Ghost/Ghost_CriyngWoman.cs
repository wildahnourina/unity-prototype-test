using UnityEngine;

public class Ghost_CriyngWoman : Ghost
{
    protected override void Awake()
    {
        base.Awake();

        //idleState = new Ghost_IdleState(this, stateMachine, "idle");
        //activeState = new Ghost_ActiveState(this, stateMachine, "head-turn");
        //chaseState = new Ghost_ChaseState(this, stateMachine, "run");
        //caughtState = new Ghost_CaughtState(this, stateMachine, "attack");
        //walkBackState = new Ghost_WalkBackState(this, stateMachine, "walk");
    }
}
