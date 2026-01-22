using UnityEngine;

public abstract class Anomaly : MonoBehaviour
{
    protected bool triggered;

    protected virtual void OnEnable() => Subscribe();
    protected virtual void OnDisable() => Unsubscribe();

    protected abstract void Subscribe();
    protected abstract void Unsubscribe();
}