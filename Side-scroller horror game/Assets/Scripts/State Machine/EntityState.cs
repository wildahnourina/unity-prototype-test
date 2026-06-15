using UnityEngine;
using Spine.Unity;
using Spine;

public abstract class EntityState
{
    protected Animator anim;
    protected Rigidbody2D rb;

    protected StateMachine stateMachine;
    protected string animBoolName;
    //protected SpineAnimator anim;
    //protected bool loopAnim = true;
    //protected TrackEntry entry;

    public EntityState NextState => null;

    public EntityState(StateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    //public EntityState(StateMachine stateMachine, SpineAnimator anim, string animName)
    //{
    //    this.stateMachine = stateMachine;
    //    this.anim = anim;
    //    this.animName = animName;
    //}

    public virtual void Enter()
    {
        //entry = anim.Play(animName, loopAnim);
        anim.SetBool(animBoolName, true);

    }

    public virtual void Update() { }
    public virtual void Exit()
    {
        //entry = null;
        anim.SetBool(animBoolName, false);
    }
}
