using UnityEngine;

public class TriggerPoint : MonoBehaviour
{
    private ObjectiveSetter objectiveSetter;
    private TriggerEmitter emitter;

    private void Awake()
    {
        TryGetComponent(out objectiveSetter);
        TryGetComponent(out emitter);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player")) return;

        emitter?.TriggerEmit();
        objectiveSetter?.SetObjective();
    }
}
