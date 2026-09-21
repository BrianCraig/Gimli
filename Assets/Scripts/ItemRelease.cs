using UnityEngine;

public class ItemRelease : MonoBehaviour, IRelease
{
    private ItemData item;
    public void Initialize(ItemData item)
    {
        this.item = item;
    }

    public ItemData Release()
    {
        Destroy(gameObject);
        return item;
    }
}
