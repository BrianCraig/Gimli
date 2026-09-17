using System;
using UnityEngine;

public class StackVertical : MonoBehaviour, IDroppable, IGrabbable
{
    public ItemData item;
    public int max_amount = 10;
    public float max_height = 1f;
    public int amount = 3;
    public GameObject base_prefab;

    bool IDroppable.CanDrop()
    {
        return false;
    }

    void IDroppable.Drop(Transform transform, Vector3 hitpoint)
    {
        throw new System.NotImplementedException();
    }

    Transform IGrabbable.Grab()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void OnValidate()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall += Regenerate;
#endif
    }

    void Regenerate()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        var height = item.height();

        for (int i = 0; i < amount; i++)
        {
            var instance = Instantiate(base_prefab, transform);
            instance.transform.SetLocalPositionAndRotation(new Vector3(0f, (height + .04f) * i, 0f), Quaternion.identity);
            instance.transform.GetChild(0).GetComponent<MeshFilter>().mesh = item.mesh;
            instance.transform.GetChild(0).GetComponent<MeshRenderer>().materials = item.materials;
        }

        var radius = item.radius();
        GetComponent<BoxCollider>().center = new Vector3(0f, max_height / 2f, 0f);
        GetComponent<BoxCollider>().size = new Vector3(radius * 2, max_height, radius * 2);
    }


#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellowGreen;
        Gizmos.matrix = transform.localToWorldMatrix;
        var height = Math.Min(max_height, max_amount * (item.height() + .04f));
        var radius = item.radius();
        Gizmos.DrawWireCube(new Vector3(0, height / 2f, 0), new Vector3(radius * 2, height, radius * 2));
    }
#endif
}
