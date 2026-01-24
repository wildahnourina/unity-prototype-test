using UnityEngine;

public struct AnomalyTriggerContext
{
    public TriggerType type;
    public Vector2 position;

    public AnomalyTriggerContext(TriggerType type, Vector2 position)
    {
        this.type = type;
        this.position = position;
    }
}

public enum TriggerType
{
    ItemPickup,
    LightOn
}
