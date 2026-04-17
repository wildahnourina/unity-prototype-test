using UnityEngine;

public abstract class Anomaly : MonoBehaviour
{
    [Header("Anomaly React To")]
    [SerializeField] private TriggerType reactTo;
    [SerializeField] private string reactToId;
    protected virtual void OnEnable()
    {
        AnomalyTriggerSignals.OnTrigger += HandleTrigger;

        var ctx = new AnomalyTriggerContext(reactTo, reactToId);

        if (AnomalyTriggerSignals.HasTriggered(ctx))
        {
            OnTriggered(ctx);
        }
    }

    protected virtual void OnDisable()
    {
        AnomalyTriggerSignals.OnTrigger -= HandleTrigger;
    }

    private void HandleTrigger(AnomalyTriggerContext ctx)
    {
        if (ctx.type != reactTo) return;

        if (reactTo == TriggerType.ItemPickup)
            if (ctx.id != reactToId) return;

        OnTriggered(ctx);
    }

    protected abstract void OnTriggered(AnomalyTriggerContext ctx);
}
