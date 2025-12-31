using System.Collections.Generic;
using System;
using UnityEngine;

public class Inventory_Player : MonoBehaviour
{
    public event Action OnInventoryChange;

    public int maxInventorySize = 5;
    public List<Inventory_Item> itemList = new List<Inventory_Item>();

    public Inventory_Item GetItemById(string itemId)
    {
        if (itemList == null) return null;

        return itemList.Find(item => item != null && item.itemData != null && item.itemData.itemId == itemId);
    }

    public bool CanAddItem(Inventory_Item itemToAdd)
    {
        bool hasStackable = FindStackable(itemToAdd) != null;
        return hasStackable || itemList.Count < maxInventorySize;
    }

    public Inventory_Item FindStackable(Inventory_Item itemToAdd)//mencari item sejenis yang belum penuh untuk menumpuk item baru ke dalam slot yang sama.
    {
        List<Inventory_Item> stackableItems = itemList.FindAll(item => item.itemData == itemToAdd.itemData);

        foreach (var stackableItem in stackableItems)
        {
            if (stackableItem.CanAddStack())
                return stackableItem;
        }

        return null;
    }

    public void AddItem(Inventory_Item itemToAdd)
    {
        Inventory_Item itemInInventory = FindStackable(itemToAdd);

        if (itemInInventory != null)
            itemInInventory.AddStack();//kalo ada itemnya, masukin ke stack, jadi nambah jumlah item yang sama (tp tetap ada stacksize)
        else
            itemList.Add(itemToAdd);// kalo gak ada, ya masuk ke slot baru

        OnInventoryChange?.Invoke();
    }

    public void RemoveOneItem(Inventory_Item itemToRemove)
    {
        Inventory_Item itemInInventory = itemList.Find(item => item == itemToRemove);

        if (itemInInventory.stackSize > 1)
            itemInInventory.RemoveStack();
        else
            itemList.Remove(itemToRemove);

        OnInventoryChange?.Invoke();
    }

}
