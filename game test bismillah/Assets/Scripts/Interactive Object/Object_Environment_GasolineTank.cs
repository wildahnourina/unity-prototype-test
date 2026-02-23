using NUnit.Framework.Interfaces;
using UnityEngine;

public class Object_Environment_GasolineTank : Object_Environment
{
    [SerializeField] private SO_ItemData full_jerrycan;
    protected override void OnInteract()
    {
        if (player == null)
            return;

        Inventory_Player inventory = player.GetComponent<Inventory_Player>();

        if (inventory == null)
            return;

        Inventory_Item itemToAdd = new Inventory_Item(full_jerrycan);

        if (inventory.CanAddItem(itemToAdd))
            inventory.AddItem(itemToAdd);

        requirement?.Relock();
    }

    protected override string GetInteractionPrompt() => "(E) Take gasoline";
}
