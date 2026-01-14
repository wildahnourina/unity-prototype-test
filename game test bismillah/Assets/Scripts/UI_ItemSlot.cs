using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler
{
    public Inventory_Item itemInSlot { get; private set; }
    protected Inventory_Player inventory;

    [Header("UI Slot Setup")]
    [SerializeField] protected GameObject defaultIcon;
    [SerializeField] protected Image itemIcon;
    [SerializeField] protected TextMeshProUGUI itemStackSize;

    protected void Awake()
    {
        inventory = FindAnyObjectByType<Inventory_Player>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (itemInSlot == null)
            return;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (itemInSlot.itemData.itemType == ItemType.Consumable)
            {
                inventory.UseItem(itemInSlot);
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            inventory.DropItem(itemInSlot);
        }
    }

    public void UpdateSlot(Inventory_Item item)
    {
        itemInSlot = item;

        if (defaultIcon != null)
            defaultIcon.gameObject.SetActive(itemInSlot == null);

        if (itemInSlot == null)
        {
            itemStackSize.text = "";
            itemIcon.color = Color.clear;
            return;
        }

        Color color = Color.white; color.a = .9f;
        itemIcon.color = color;
        itemIcon.sprite = itemInSlot.itemData.itemIcon;
        itemStackSize.text = item.stackSize > 1 ? item.stackSize.ToString() : "";
    }
}
