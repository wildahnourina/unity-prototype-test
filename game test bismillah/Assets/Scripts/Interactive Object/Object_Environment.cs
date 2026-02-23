using UnityEngine;
using UnityEngine.UIElements;

public abstract class Object_Environment : Object_Interactable
{
    protected Object_RequireItem requirement;

    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out requirement);
    }

    public override void Interact()
    {
        Inventory_Player inventory = player.GetComponent<Inventory_Player>();

        if (requirement != null)
        {
            if (!requirement.TryUnlock(inventory))
            {
                objectiveSetter?.SetObjective();
                AudioManager.instance.PlayGlobalSFX("object_locked");
                return;
            }

            RefreshPrompt();
        }

        OnInteract();
    }

    protected override string GetPromptText()
    {
        if (requirement != null && requirement.isLocked)
            return GetLockedPrompt();

        return GetInteractionPrompt();
    }
    protected virtual string GetLockedPrompt() => GetInteractionPrompt(); //init buat yang gak perlu override method ini
    protected abstract void OnInteract();
    protected abstract string GetInteractionPrompt();
}
