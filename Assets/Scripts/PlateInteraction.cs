using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateInteraction : MonoBehaviour, IDrop, IRelease
{
    private readonly Stack<ItemData> items = new();
    public bool Drop(Vector3 hitpoint, ItemData item)
    {
        items.Push(item);
        Regenerate();
        return true;
    }

    public ItemData Release()
    {
        items.TryPop(out var item);
        Regenerate();
        return item;
    }

    void Regenerate()
    {
        // Ignores first child gameobject since its the plate
        // We should move this to a items[] renderer to simplify
        for (int i = transform.childCount - 1; i >= 1; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        var total_height = 0.05f;  
        foreach(var item in items.Reverse())
        {
            var go = Game.InstantiateItem(item, transform);
            go.transform.position += new Vector3(0, total_height, 0);
            total_height += item.height() + .01f;
        }
    }
}
