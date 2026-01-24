using UnityEngine;

public class Anomaly_Ghost : Anomaly
{
    [SerializeField] private Ghost ghost;
    protected override void OnTriggered(AnomalyTriggerContext ctx)
    {
        ghost.Trigger();
    }
}
