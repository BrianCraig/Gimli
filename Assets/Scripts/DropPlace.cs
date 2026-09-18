using UnityEngine;

public class DropItem : MonoBehaviour, IDroppable, IGrabbable
{
    private ItemData item = null;

    public ItemData Grab()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        var current_item = item;
        item = null;
        return current_item;
    }

    bool IDroppable.Drop(Vector3 hitpoint, ItemData incoming_item)
    {
        if (item != null)
        {
            return false;
        }
        item = incoming_item;
        var instance = Instantiate(Game.base_prefab, transform);
        instance.transform.GetChild(0).GetComponent<MeshFilter>().mesh = item.mesh;
        instance.transform.GetChild(0).GetComponent<MeshRenderer>().materials = item.materials;
        instance.transform.SetLocalPositionAndRotation(Vector3.zero, new Quaternion(-0.181592122f, -0.144707561f, 0.0379015729f, 0.971929789f));
        instance.transform.localScale = new Vector3(0.460830003f, 0.460830003f, 0.460830003f);
        return true;
    }
}
