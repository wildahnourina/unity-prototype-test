using UnityEngine;

public class Anomaly_Ghost : Anomaly
{
    protected override void OnTriggered(AnomalyTriggerContext ctx)
    {
        GetComponent<Ghost>().Trigger();
    }
}
