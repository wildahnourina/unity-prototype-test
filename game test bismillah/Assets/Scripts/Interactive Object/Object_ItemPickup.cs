using UnityEngine;

public class Object_ItemPickup : Object_Interactable
{
    [SerializeField] private SO_ItemData itemData;
    [SerializeField] private Vector2 dropForce = new Vector2(3, 10);

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;

    private TriggerEmitter itemPickup_emitter;

    protected override void Awake()
    {
        base.Awake();

        TryGetComponent(out itemPickup_emitter);
    }
    public string ItemID => itemData.itemId;

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

        rb.bodyType = RigidbodyType2D.Kinematic;
        col.isTrigger = true;
    }

    public void ApplyDropForce()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;

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

    public override void Interact(Player player)
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
            AudioManager.instance.PlayGlobalSFX("item_pickup");

            itemPickup_emitter?.TriggerEmit();
            Destroy(gameObject);
        }
    }

    protected override string GetPromptText()
    {
        return "(E) Pick up";
    }
}


//kalau emitter lebih dari 1

//private Dictionary<TriggerType, TriggerEmitter> emitters;

//void Awake()
//{
//    emitters = new();

//    foreach (var emitter in GetComponents<TriggerEmitter>())
//    {
//        emitters[emitter.TriggerType] = emitter;
//    }
//}

//emitters[TriggerType.ItemPickup].TriggerEmit();

//atau

//if (emitters.TryGetValue(TriggerType.ItemPickup, out var emitter))
//{
//    emitter.TriggerEmit();
//}