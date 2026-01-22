using UnityEngine;

[RequireComponent(typeof(LightGroup))]
public class Anomaly_OnTimer_LightFlicker : Anomaly_OnTimer
{
    [SerializeField] float flickerDuration = 1.2f;

    protected override void OnTriggered()
    {
        GetComponent<LightGroup>().StartFlicker(flickerDuration);
    }
}
