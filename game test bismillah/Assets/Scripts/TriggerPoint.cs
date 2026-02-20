using UnityEngine;

public class TriggerPoint : MonoBehaviour
{
    private ObjectiveSetter objectiveSetter;
    private AnomalyTriggerEmitter emitter;

    private void Awake()
    {
        TryGetComponent(out objectiveSetter);
        TryGetComponent(out emitter);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        emitter?.TriggerEmit();
        objectiveSetter?.SetObjective();
    }
}
