using UnityEngine;

public abstract class TriggerReaction : MonoBehaviour
{
    [Header("React To")]
    [SerializeField] private TriggerType reactTo;
    [SerializeField] private string reactToId;
    protected virtual void OnEnable()
    {
        TriggerSignals.OnTrigger += HandleTrigger;

        var ctx = new TriggerContext(reactTo, reactToId);

        if (TriggerSignals.HasTriggered(ctx))
        {
            OnTriggered(ctx);
        }
    }

    protected virtual void OnDisable()
    {
        TriggerSignals.OnTrigger -= HandleTrigger;
    }

    private void HandleTrigger(TriggerContext ctx)
    {
        if (ctx.type != reactTo) return;

        if (reactTo == TriggerType.ItemPickup)
            if (ctx.id != reactToId) return;

        OnTriggered(ctx);
    }

    protected abstract void OnTriggered(TriggerContext ctx);
}
