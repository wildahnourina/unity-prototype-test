using UnityEngine;

public class Object_ItemInside : MonoBehaviour
{
    [SerializeField] private GameObject itemInsidePrefab;
    [SerializeField] private SO_ItemData itemData;
    [SerializeField] private Transform spawnPoint;

    private GameObject spawnedItem;
    private bool hasSpawned;

    private void Awake()
    {
        Debug.Assert(spawnPoint != null, $"{name} missing Spawn Point");
    }

    public void ShowItem()
    {
        if (itemData == null) return;

        // item udah diambil player
        if (hasSpawned && spawnedItem == null)
            return;

        if (!hasSpawned)
        {
            spawnedItem = Instantiate(itemInsidePrefab, spawnPoint.position, Quaternion.identity, transform.parent);
            spawnedItem.GetComponent<Object_ItemPickup>().SetupItem(itemData);

            hasSpawned = true;
        }

        spawnedItem.SetActive(true);
    }

    public void HideItem()
    {
        if (spawnedItem == null) return;

        spawnedItem.SetActive(false);
    }
}
