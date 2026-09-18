using UnityEngine;

public interface IDroppable
{
    /// <summary>
    /// Possibly drops an item, returns false if it can't be dropped.
    /// </summary>
    /// <param name="hitpoint">the collided world vec3 hit</param>
    /// <param name="item">the item to be dropped</param>
    public bool Drop(Vector3 hitpoint, ItemData item);
}