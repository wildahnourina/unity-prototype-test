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

        if (inventory.GetItemCount(full_jerrycan) >= 2)
            GameManager.instance.task1Completed = true;

        requirement?.Relock();

        //empty_jerrycan nya dihapus di requireItem
    }

    protected override string GetInteractionPrompt() => "(E) Take gasoline";
}
