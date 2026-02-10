using UnityEngine;

public class Object_Lockable : MonoBehaviour
{
    [SerializeField] private string requiredKeyId;

    public bool isLocked { get; private set; } = true;

    public bool TryUnlock(Inventory_Player inventory)
    {
        if (!isLocked)
            return true;

        Inventory_Item item = inventory.GetItemById(requiredKeyId);

        if (item == null)
        {
            AudioManager.instance.PlayGlobalSFX("object_locked");
            return false;
        }

        isLocked = false;
        inventory.RemoveOneItem(item);

        return true;
    }
}
