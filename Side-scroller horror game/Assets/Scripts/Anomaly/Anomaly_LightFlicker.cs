using UnityEngine;

public class Anomaly_LightFlicker : Anomaly
{
    [SerializeField] float flickerDuration = 1.2f;

    protected override void OnTriggered(TriggerContext ctx)
    {
        GetComponent<LightGroup>().StartFlicker(flickerDuration);
    }
}
