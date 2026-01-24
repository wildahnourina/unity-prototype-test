using System;
using UnityEngine;

public static class AnomalyTriggerSignals
{
    public static Action OnFlashlightEmpty;
    public static Action<string> OnItemPicked;

    public static Action<AnomalyTriggerContext> OnTrigger;

    public static void RaiseTrigger(AnomalyTriggerContext ctx)
    {
        OnTrigger?.Invoke(ctx);
    }
}
