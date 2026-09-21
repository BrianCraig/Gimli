using UnityEngine;

public class DropSurface : MonoBehaviour, IDrop
{
    public bool Drop(Vector3 hitpoint, ItemData item)
    {
        var instance = Game.InstantiateItem(item, transform, GameInstantiateItemFlags.Grabbable);
        instance.transform.SetPositionAndRotation(hitpoint, Quaternion.identity);
        return true;
    }
}
