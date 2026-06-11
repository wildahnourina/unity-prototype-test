using UnityEngine;

public class TriggerEmitter : MonoBehaviour
{
    [SerializeField] private TriggerType triggerType;

    private Object_ItemPickup itemPickup;

    private void Awake()
    {
        TryGetComponent(out itemPickup);
    }

    public void TriggerEmit()
    {
        var id = "";

        if (triggerType == TriggerType.ItemPickup && itemPickup != null)
            id = itemPickup?.ItemID;

        TriggerSignals.RaiseTrigger(new TriggerContext(triggerType, id));
    }
}
//panggil triggeremit di setiap objek yang mentrigger
