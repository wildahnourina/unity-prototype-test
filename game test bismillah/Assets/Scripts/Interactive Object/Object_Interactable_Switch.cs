using UnityEngine;

public class Object_Interactable_Switch : Object_Interactable
{
    [SerializeField] private LightGroup lightGroup;

    private bool isOn;

    protected override void Awake()
    {
        base.Awake();

        isOn = lightGroup.IsOn;
    }

    public override void Interact()
    {
        if (lightGroup == null) return;

        lightGroup.Toggle();
        RefreshPrompt();
    }

    protected override string GetPromptText()
    {
        return lightGroup.IsOn ? "(E) Switch OFF" : "(E) Switch ON";
    }
}
