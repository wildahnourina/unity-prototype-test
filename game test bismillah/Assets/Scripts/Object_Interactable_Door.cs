using UnityEngine;

public class Object_Interactable_Door : Object_Interactable
{
    [SerializeField] private string requiredKeyId;

    private bool isLocked = true;
    private bool isOpen;

    public override void Interact()
    {
        Inventory_Player inventory = player.GetComponent<Inventory_Player>();

        if (isLocked)
        {
            Inventory_Item item = inventory.GetItemById(requiredKeyId);

            if (item == null) 
                return;

            isLocked = false;
            inventory.RemoveOneItem(item);
        }

        isOpen = !isOpen;        
        RefreshPrompt();
    }

    protected override string GetPromptText()
    {
        if (isLocked)
            return "(E) Enter the key";

        return isOpen ? "(E) Close" : "(E) Open";
    }
}
