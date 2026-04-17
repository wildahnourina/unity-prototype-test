using UnityEngine;

public struct AnomalyTriggerContext
{
    public TriggerType type;
    public string id;

    public AnomalyTriggerContext(TriggerType type, string id)
    {
        this.type = type;
        this.id = id;
    }
}

public enum TriggerType
{
    ItemPickup,
    RoomLightOn,
    FlashlightNear,
    EnteredPointer
}
