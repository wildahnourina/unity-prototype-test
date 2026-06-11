using UnityEngine;
using UnityEngine.UIElements;

public class Object_Environment_Locker : Object_Environment
{
    private bool isOpen;
    private Object_ItemInside itemInside;

    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out itemInside);
    }

    protected override void OnInteract()
    {
        isOpen = !isOpen;

        if (isOpen) OpenLocker();
        else CloseLocker();

        RefreshPrompt();
    }

    private void OpenLocker()
    {
        // animasi buka
        itemInside?.ShowItem();
    }

    private void CloseLocker()
    {
        // animasi tutup
        itemInside?.HideItem();
    }

    protected override string GetInteractionPrompt()
    {
        return isOpen ? "(E) Close" : "(E) Open";
    }
}
