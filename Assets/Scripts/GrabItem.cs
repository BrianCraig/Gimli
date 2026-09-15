using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabItem : MonoBehaviour
{
    LayerMask grabLayer, depositLayer;
    public GameObject cam;
    private Transform grabbing;
    [SerializeField] InputActionReference interact;
    [SerializeField] TextMeshPro actionsText;

    void Awake()
    {
        grabLayer = LayerMask.GetMask("Grab");
        depositLayer = LayerMask.GetMask("Deposit");
    }

    void Update()
    {
        actionsText.text = "";
        var cam_transform = cam.transform;
        if (EmptyHanded())
        {
            if (Physics.Raycast(cam_transform.position, cam_transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, grabLayer))
            {
                var grabbable = hit.transform.GetComponentInParent<IGrabbable>();
                if (grabbable != null)
                {

                    if (interact.action.WasPressedThisFrame())
                    {
                        grabbing = grabbable.Grab();
                        grabbing.SetParent(transform);
                        grabbing.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                        grabbing.localScale = Vector3.one;
                    }
                    else
                    {
                        actionsText.text = $"[{interact.action.GetBindingDisplayString()}] Grab";
                    }
                }
            }
        }
        else
        {
            if (Physics.Raycast(cam_transform.position, cam_transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, depositLayer))
            {
                var droppable = hit.transform.GetComponentInParent<IDroppable>();
                if (droppable != null && droppable.CanDrop())
                {
                    if (interact.action.WasPressedThisFrame())
                    {
                        droppable.Drop(grabbing);
                        grabbing = null;
                    }
                    else
                    {
                        actionsText.text = $"[{interact.action.GetBindingDisplayString()}] Drop";
                    }
                }
            }
        }
    }

    bool EmptyHanded()
    {
        return grabbing == null;
    }
}
