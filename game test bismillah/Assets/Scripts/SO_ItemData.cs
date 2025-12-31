using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Item/Item Data")]
public class SO_ItemData : ScriptableObject
{
    public string itemId;
    public string itemName;
    public Sprite itemIcon;

    [TextArea]
    public string description;

    public ItemType itemType;
    public bool stackable;
    public int maxStack;
}

public enum ItemType
{
    KeyItem,
    Consumable,
}
