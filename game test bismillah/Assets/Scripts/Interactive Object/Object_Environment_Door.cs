using UnityEngine;

public class Object_Environment_Door : Object_Environment
{
    private bool isOpen =  false;

    protected override void OnInteract()
    {
        if (!isOpen)
        {
            isOpen = true;
            RefreshPrompt();
            return;
        }

        Debug.Log("Entered room");
        isOpen = false;
        RefreshPrompt();

        //ganti scene
    }

    protected override string GetInteractionPrompt()
    {
        return isOpen ? "(E) Enter room" : "(E) Open";
    }
}
