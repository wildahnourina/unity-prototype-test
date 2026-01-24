using UnityEngine;

public class Object_ItemPickup : Object_Interactable
{
    [SerializeField] private SO_ItemData itemData;
    [SerializeField] private Vector2 dropForce = new Vector2(3, 10);

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;

    private void OnValidate()
    {
        if (itemData == null)
            return;

        sr = GetComponentInChildren<SpriteRenderer>();
        SetupVisual();
    }

    public void SetupItem(SO_ItemData itemData)
    {
        this.itemData = itemData;
        SetupVisual();

        float xDropForce = Random.Range(-dropForce.x, dropForce.x);
        rb.linearVelocity = new Vector2(xDropForce, dropForce.y);
        col.isTrigger = false;
    }

    private void SetupVisual()
    {
        sr.sprite = itemData.itemIcon;
        gameObject.name = "Object_ItemPickup - " + itemData.itemName;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && col.isTrigger == false)
        {
            col.isTrigger = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public override void Interact()
    {
        if (player == null)
            return;

        Inventory_Player inventory = player.GetComponent<Inventory_Player>();

        if (inventory == null)
            return;

        Inventory_Item itemToAdd = new Inventory_Item(itemData);

        if (inventory.CanAddItem(itemToAdd))
        {
            inventory.AddItem(itemToAdd);

            EmitTrigger();
            Destroy(gameObject);
        }
    }

    protected override string GetPromptText()
    {
        return "(E) Pick up";
    }
}
