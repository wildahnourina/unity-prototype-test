using System;
using System.Collections;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager instance;
    public event Action<string> OnObjectiveChanged;

    private Coroutine ClearCo;

    void Awake()
    {
        instance = this;
    }

    public void ShowObjective(string text, float duration = 3f)
    {
        if (ClearCo != null)
            StopCoroutine(ClearCo);

        OnObjectiveChanged?.Invoke(text);
        ClearCo = StartCoroutine(ClearAfterDelayCo(duration));
    }

    private IEnumerator ClearAfterDelayCo(float delay)
    {
        yield return new WaitForSeconds(delay);
        OnObjectiveChanged?.Invoke("");
    }
}
