using System;
using UnityEngine;

[Serializable]
public class Inventory_Item
{
    //private string itemId;
    public SO_ItemData itemData;
    public int stackSize = 1;

    public Inventory_Item(SO_ItemData itemData)
    {
        this.itemData = itemData;
        //itemId = itemData.itemName + " - " + Guid.NewGuid();
    }
    public bool CanAddStack() => stackSize < itemData.maxStack;
    public void AddStack() => stackSize++;
    public void RemoveStack() => stackSize--;
}
