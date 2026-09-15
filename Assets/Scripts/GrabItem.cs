using UnityEngine;

public class GrabItem : MonoBehaviour
{
    LayerMask layerMask;
    public GameObject cam;

    void Awake()
    {
        layerMask = LayerMask.GetMask("Object");
    }

    void FixedUpdate()
    {
        var cam_transform = cam.transform;
        if (Physics.Raycast(cam_transform.position, cam_transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            var grabbable = hit.transform.GetComponentInParent<IGrabbable>();
            if (grabbable != null)
            {
                var obj_transform = grabbable.Grab();
                obj_transform.SetParent(transform);
                obj_transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                obj_transform.localScale = Vector3.one;
            }
        }
    }
}
