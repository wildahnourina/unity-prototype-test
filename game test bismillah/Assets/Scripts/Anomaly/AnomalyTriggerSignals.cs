using System;
using System.Collections.Generic;
using UnityEngine;

public static class AnomalyTriggerSignals
{
    public static Action<AnomalyTriggerContext> OnTrigger;
    private static HashSet<string> triggered = new();

    public static void RaiseTrigger(AnomalyTriggerContext ctx)
    {
        string key = $"{ctx.type}_{ctx.id}";
        triggered.Add(key);// simpan state

        OnTrigger?.Invoke(ctx);
    }

    public static bool HasTriggered(AnomalyTriggerContext ctx)
    {
        string key = $"{ctx.type}_{ctx.id}";
        return triggered.Contains(key);
    }
}
