using UnityEngine;

public interface IDroppable
{
    public bool CanDrop();
    public void Drop(Transform transform, Vector3 hitpoint);
}