using UnityEngine;

public class Object_Environment_Door : Object_Environment
{
    private bool isOpen =  false;

    //private string sceneName

    protected override void OnInteract()
    {
        if (!isOpen)
        {
            AudioManager.instance.PlayGlobalSFX("door_open");

            isOpen = true;
            RefreshPrompt();
            return;
        }

        Debug.Log("Entered room");
        isOpen = false;
        RefreshPrompt();
        AudioManager.instance.PlayGlobalSFX("door_close");

        //ganti scene
    }

    protected override string GetLockedPrompt() => "(E) Enter the key";

    protected override string GetInteractionPrompt()
    {
        return isOpen ? "(E) Enter room" : "(E) Open";
    }
}
