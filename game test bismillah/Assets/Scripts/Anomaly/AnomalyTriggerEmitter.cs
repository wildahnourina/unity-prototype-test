using UnityEngine;

public class AnomalyTriggerEmitter : MonoBehaviour
{
    [SerializeField] private TriggerType triggerType;

    public void TriggerEmit()
    {
        AnomalyTriggerSignals.RaiseTrigger(new AnomalyTriggerContext(triggerType, transform.position));
    }
}
//panggil triggeremit di setiap objek yang mentrigger
