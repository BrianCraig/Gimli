using UnityEngine;

public class DropSurface : MonoBehaviour, IDroppable
{
    public bool CanDrop()
    {
        return true;
    }

    public void Drop(Transform incoming_transform, Vector3 hitpoint)
    {
        incoming_transform.SetParent(transform.parent);
        incoming_transform.SetPositionAndRotation(hitpoint, Quaternion.identity);
        incoming_transform.localScale = Vector3.one;
    }
}
