using UnityEngine;

public abstract class EntityState
{
    protected Rigidbody2D rb;

    protected StateMachine stateMachine;
    protected string animName;
    protected SpineAnimator anim;

    public EntityState(StateMachine stateMachine, SpineAnimator anim, string animName)
    {
        this.stateMachine = stateMachine;
        this.anim = anim;
        this.animName = animName;
    }

    public virtual void Enter()
    {
        anim.Play(animName);
    }

    public virtual void Update() { }
    public virtual void Exit()
    {

    }
}
