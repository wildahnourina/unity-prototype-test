using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Object_Environment_Car : Object_Environment
{
    private bool canUse = false;
    protected override void OnInteract()
    {
        if (!canUse)
        {
            //AudioManager.instance.PlayGlobalSFX("door_open");
            //open animation
            canUse = true;
            RefreshPrompt();
            return;
        }

        canUse = false;
        RefreshPrompt();
        //AudioManager.instance.PlayGlobalSFX("door_close");
        //tamat ganti scene main menu
    }

    protected override string GetLockedPrompt() => "(E) Put in gasoline";
    protected override string GetInteractionPrompt()
    {
        return canUse ? "(E) Go!" : "(E) Open";
    }
}
