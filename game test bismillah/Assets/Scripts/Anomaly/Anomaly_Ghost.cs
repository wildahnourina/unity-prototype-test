using UnityEngine;

public class Anomaly_Ghost : Anomaly
{
    protected override void OnTriggered(TriggerContext ctx)
    {
        GetComponent<Ghost>().Trigger();
    }
}
