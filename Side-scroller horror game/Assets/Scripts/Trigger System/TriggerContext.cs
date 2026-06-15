using UnityEngine;

public struct TriggerContext
{
    public TriggerType type;
    public string id;

    public TriggerContext(TriggerType type, string id)
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
    GameStarted
}
