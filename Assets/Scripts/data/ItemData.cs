using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public Mesh mesh;
    public Material[] materials;

    public MonoScript onReleaseOverride;

    public float height()
    {
        // We use Z and not Y since they are probably from Blender, which uses Z for up.
        return mesh.bounds.max.z;
    } 

    public float radius()
    {
        return Math.Max(mesh.bounds.max.x, mesh.bounds.max.y);
    } 
}
