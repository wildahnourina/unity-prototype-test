using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Game Setup/Item Data/New Item", fileName = "Item - ")]
public class SO_ItemData : ScriptableObject
{
    public string itemId;
    public string itemName;
    public Sprite itemIcon;

    [TextArea]
    public string description;

    public ItemType itemType;
    public int maxStack;
}

public enum ItemType
{
    KeyItem,
    Consumable,
}
