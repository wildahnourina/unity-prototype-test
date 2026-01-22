using System.Collections.Generic;
using UnityEngine;

public class UI_ItemSlotParent : MonoBehaviour
{
    private UI_ItemSlot[] slots;

    public void UpdateSlots(List<Inventory_Item> itemList)//masukkan yang ada di item list ke ui slot
    {
        if (slots == null)
            slots = GetComponentsInChildren<UI_ItemSlot>();

        for (int i = 0; i < slots.Length; i++)//ui slot nya yang ngecek item list
        {
            if (i < itemList.Count)
            {
                slots[i].UpdateSlot(itemList[i]);
            }
            else
            {
                slots[i].UpdateSlot(null);//kalo itemlist nya cuma 2, ya slot 3 dst diisi null
            }
        }
    }
}
