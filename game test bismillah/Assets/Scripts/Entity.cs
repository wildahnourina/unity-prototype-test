using UnityEngine;
using Spine.Unity;

public class Entity : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Collider2D col { get; private set; }

    public StateMachine stateMachine;
    protected SkeletonAnimation skeletonAnim;
    protected SpineAnimator anim;


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        skeletonAnim = GetComponentInChildren<SkeletonAnimation>();
        anim = new SpineAnimator(skeletonAnim);

        stateMachine = new StateMachine();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        stateMachine.UpdateActiveState();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
    }
}
