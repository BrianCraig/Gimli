using static System.Linq.Enumerable;
using UnityEngine;
using System.Collections.Generic;

public class VisibleTrigger : MonoBehaviour
{
    public Vector3 size = new(10, 5, 10);
    public Vector3 origin = new(0, 0, 0);
    public bool debug_gizmos = true;
    private bool triggered = false;
    private List<(Vector3, bool)> trigger_points = new();

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (!debug_gizmos) return;

        Gizmos.color = triggered ? Color.green : Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(origin, size);

        Gizmos.color = Color.green;
        foreach (var (point, triggered) in trigger_points)
        {
            if (triggered) Gizmos.DrawSphere(point, 0.1f);
        }
    }
#endif

    void Update()
    {
        trigger_points = new();

        Camera cam = Camera.main!;
        var any_visible = false;

        // Check is based on points, we should use planes instead but this is a first approximation
        foreach (var x in Range(-2, 5))
        {
            foreach (var y in Range(-2, 5))
            {
                foreach (var z in Range(-2, 5))
                {
                    var translation = new Vector3(x, y, z);
                    translation.Scale(size / 4f);
                    var point = origin + translation;
                    var is_visible = IsPointVisible(cam, point);
                    if (is_visible)
                    {
                        any_visible = true;
                    }

                    trigger_points.Add((point, is_visible));
                }
            }
        }

        triggered = any_visible;

    }

    private bool IsPointVisible(Camera camera, Vector3 origin)
    {
        Vector4 clip = camera.projectionMatrix * camera.worldToCameraMatrix * transform.localToWorldMatrix * new Vector4(origin.x, origin.y, origin.z, 1f);
        if (clip.w <= 0)
        {
            return false;
        }

        Vector3 ndc = new Vector3(clip.x, clip.y, clip.z) / clip.w;

        return
            ndc.x >= -1f && ndc.x <= 1f &&
            ndc.y >= -1f && ndc.y <= 1f &&
            ndc.z >= -1f && ndc.z <= 1f;
    }
}
