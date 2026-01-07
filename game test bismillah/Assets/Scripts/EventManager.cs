using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    private static Dictionary<string, Action> events = new();

    public static void Subscribe(string eventName, Action listener)
    {
        if (!events.ContainsKey(eventName))
            events[eventName] = listener;
        else
            events[eventName] += listener;
    }

    public static void Unsubscribe(string eventName, Action listener)
    {
        if (!events.ContainsKey(eventName)) return;

        events[eventName] -= listener;
    }

    public static void Trigger(string eventName)
    {
        if (events.TryGetValue(eventName, out var action))
        {
            action?.Invoke();
            Debug.Log($"[Event Triggered] {eventName}");
        }
    }
}
