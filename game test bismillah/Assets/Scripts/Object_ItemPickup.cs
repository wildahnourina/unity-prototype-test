using UnityEngine;

public class Object_ItemPickup : Object_Interactable
{
    [SerializeField] private SO_ItemData itemData;

    [SerializeField] private SpriteRenderer sr;
    //[SerializeField] private GameObject interactToolTip;

    private void OnValidate()
    {
        if (itemData == null)
            return;

        sr = GetComponentInChildren<SpriteRenderer>();
        sr.sprite = itemData.itemIcon;
        gameObject.name = "Object_ItemPickup - " + itemData.itemName;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (!collision.TryGetComponent(out Player player))
    //        return;
    //    this.player = player;

    //    interactToolTip.SetActive(true);
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.TryGetComponent(out Player player) && this.player == player)
    //        this.player = null;

    //    interactToolTip.SetActive(false);
    //}

    public override void Interact()
    {
        Debug.Log("masuk inventory");
        if (player == null)
            return;

        Inventory_Player inventory = player.GetComponent<Inventory_Player>();

        if (inventory == null)
            return;

        Inventory_Item itemToAdd = new Inventory_Item(itemData);

        if (inventory.CanAddItem(itemToAdd))
        {
            inventory.AddItem(itemToAdd);
            Destroy(gameObject);
        }
    }

    protected override string GetPromptText()
    {
        return "(E) Pick up";
    }
}
