using UnityEngine;
using UnityEngine.UIElements;

public abstract class Object_Environment : Object_Interactable
{
    public override void Interact()
    {
        Inventory_Player inventory = player.GetComponent<Inventory_Player>();

        if (TryGetComponent<Object_Lockable>(out var lockable))
        {
            if (!lockable.TryUnlock(inventory))
                return;

            RefreshPrompt();
        }

        OnInteract();
    }

    protected override string GetPromptText()
    {
        if (TryGetComponent<Object_Lockable>(out var lockable) && lockable.isLocked)
            return "(E) Enter the key";

        return GetInteractionPrompt();
    }

    protected abstract void OnInteract();
    protected abstract string GetInteractionPrompt();
}
