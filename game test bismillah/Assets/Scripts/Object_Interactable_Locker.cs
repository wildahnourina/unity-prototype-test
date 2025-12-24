using UnityEngine;

public class Object_Interactable_Locker : Object_Interactable
{
    private bool isOpen;
    public override void Interact()
    {
        isOpen = !isOpen;
        RefreshPrompt();

        //anim locker
    }

    protected override string GetPromptText()
    {
        return isOpen ? "(E) Close" : "(E) Open";
    }
}
