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

    [Header("Consumable")]
    public ItemEffect itemEffect;

    [Header("Equipable")]
    public EquipmentItem equipmentItem;
}

public enum ItemType
{
    KeyItem,
    Consumable,
    Equipable
}

public enum ConsumableItem
{
    None,
    Battery
}

public enum EquipmentItem
{
    None,
    Flashlight
}

[System.Serializable]
public class ItemEffect
{
    public ConsumableItem consumableType = ConsumableItem.None;
    public float effectValue;

    public bool TryExecuteEffect(Player player)
    {
        switch (consumableType)
        {
            case ConsumableItem.Battery:
                return player.flashlight.TryIncreaseBattery(effectValue);
        }

        return false;
    }
}
