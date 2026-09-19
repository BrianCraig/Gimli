using UnityEngine;

public class DropSurface : MonoBehaviour, IDrop
{
    public bool Drop(Vector3 hitpoint, ItemData item)
    {
        var instance = Instantiate(Game.base_prefab, transform);
        instance.transform.GetChild(0).GetComponent<MeshFilter>().mesh = item.mesh;
        instance.transform.GetChild(0).GetComponent<MeshRenderer>().materials = item.materials;
        instance.transform.SetPositionAndRotation(hitpoint, Quaternion.identity);
        return true;
    }
}
