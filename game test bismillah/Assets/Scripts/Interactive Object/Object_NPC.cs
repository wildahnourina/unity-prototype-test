using Spine.Unity;
using UnityEngine;

public class Object_NPC : Object_Interactable
{
    [SerializeField] private Transform npc;
    private bool facingRight = true;

    private SkeletonAnimation skeleton;

    protected override void Awake()
    {
        base.Awake();
        skeleton = npc.GetComponent<SkeletonAnimation>();
    }

    private void Start()
    {
        skeleton.AnimationState.SetAnimation(0, "idle", true);
    }

    private void Update()
    {
        HandleNpcFlip();
    }

    private void HandleNpcFlip()
    {
        if (player == null || npc == null)
            return;

        if (npc.position.x > player.transform.position.x && facingRight)
        {
            npc.transform.Rotate(0, 180, 0);
            facingRight = false;
        }
        else if (npc.position.x < player.transform.position.x && facingRight == false)
        {
            npc.transform.Rotate(0, 180, 0);
            facingRight = true;
        }
    }

    public override void Interact()
    {
        
    }

    protected override string GetPromptText()
    {
        return "(E) Talk";
    }
}
