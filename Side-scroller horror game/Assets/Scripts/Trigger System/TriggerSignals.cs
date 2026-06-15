using System;
using System.Collections.Generic;
using UnityEngine;

public static class TriggerSignals
{
    public static Action<TriggerContext> OnTrigger;
    private static HashSet<string> triggered = new();

    public static void RaiseTrigger(TriggerContext ctx)
    {
        string key = $"{ctx.type}_{ctx.id}";
        triggered.Add(key);// simpan state

        OnTrigger?.Invoke(ctx);
    }

    public static bool HasTriggered(TriggerContext ctx)
    {
        string key = $"{ctx.type}_{ctx.id}";
        return triggered.Contains(key);
    }
}
