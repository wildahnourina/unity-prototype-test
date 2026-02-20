using UnityEngine;
using System;

public class ObjectiveSetter : MonoBehaviour
{
    [TextArea] 
    [SerializeField] private string objectiveText;
    [SerializeField] private float duration = 3f;

    public void SetObjective()
    {
        ObjectiveManager.instance.ShowObjective(objectiveText, duration);
    }
}
