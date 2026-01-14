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

    public ItemEffect itemEffect;
}

public enum ItemType
{
    KeyItem,
    Consumable,
    Equipment
}

public enum ConsumableType
{
    None,
    Battery
}

[System.Serializable]
public class ItemEffect
{
    public ConsumableType consumableType = ConsumableType.None;
    public float effectValue;

    public bool TryExecuteEffect(Player player)
    {
        switch (consumableType)
        {
            case ConsumableType.Battery:
                return player.flashlight.TryIncreaseBattery(effectValue);
        }

        return false;
    }
}
