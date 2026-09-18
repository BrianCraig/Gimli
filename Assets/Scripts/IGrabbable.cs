using UnityEngine;

public interface IGrabbable
{
    /// <summary>
    /// Possibly grabs an item 
    /// </summary>
    /// <returns>an ItemData or null if can't get any item</returns>
    public ItemData Grab();
}