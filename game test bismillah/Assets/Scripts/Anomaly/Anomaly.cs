using UnityEngine;

public abstract class Anomaly : MonoBehaviour
{
    public TriggerType reactTo;
    protected virtual void OnEnable()
    {
        AnomalyTriggerSignals.OnTrigger += HandleTrigger;
    }

    protected virtual void OnDisable()
    {
        AnomalyTriggerSignals.OnTrigger -= HandleTrigger;
    }

    private void HandleTrigger(AnomalyTriggerContext ctx)
    {
        if (ctx.type != reactTo) return;
        OnTriggered(ctx);
    }

    protected abstract void OnTriggered(AnomalyTriggerContext ctx);
}
