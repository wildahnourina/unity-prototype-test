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

    public override void Interact(Player player)
    {
        if (lightGroup == null) return;

        lightGroup.Toggle();
        isOn = lightGroup.IsOn;
        AudioManager.instance.PlayGlobalSFX("toggle");
        RefreshPrompt();
    }

    protected override string GetPromptText()
    {
        return isOn ? "(E) Switch OFF" : "(E) Switch ON";
    }
}
