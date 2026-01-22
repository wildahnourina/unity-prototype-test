using UnityEngine;
using System.Collections;

public abstract class Anomaly_OnTimer : Anomaly
{
    [SerializeField] protected float delay = 10f;
    Coroutine timerCo;

    protected override void Subscribe()
    {
        if (timerCo != null) return;
        timerCo = StartCoroutine(Timer());
    }

    protected override void Unsubscribe()
    {
        if (timerCo != null)
        {
            StopCoroutine(timerCo);
            timerCo = null;
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(delay);

        if (triggered) yield break;
        triggered = true;

        OnTriggered();
    }

    protected abstract void OnTriggered();
}
