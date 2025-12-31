using UnityEngine;
using UnityEngine.UIElements;

public class Object_Environment_Locker : Object_Environment
{
    private bool isOpen;

    protected override void OnInteract()
    {
        isOpen = !isOpen;
        RefreshPrompt();
    }

    protected override string GetInteractionPrompt()
    {
        return isOpen ? "(E) Close" : "(E) Open";
    }
}
