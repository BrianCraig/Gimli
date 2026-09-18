using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabItem : MonoBehaviour
{
    LayerMask grabLayer;
    public GameObject cam;
    private ItemData grabbing;
    [SerializeField] InputActionReference interact;
    [SerializeField] TextMeshPro actionsText;

    void Awake()
    {
        grabLayer = LayerMask.GetMask("Grab");
    }

    void Update()
    {
        actionsText.text = "";
        var cam_transform = cam.transform;


        if (Physics.Raycast(cam_transform.position, cam_transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, grabLayer))
        {
            if (EmptyHanded())
            {
                var grabbable = hit.transform.GetComponentInParent<IGrabbable>();
                if (grabbable != null)
                {

                    if (interact.action.WasPressedThisFrame())
                    {
                        var item = grabbable.Grab();
                        if (item != null)
                        {
                            var instance = Instantiate(Game.base_prefab, transform);
                            instance.transform.GetChild(0).GetComponent<MeshFilter>().mesh = item.mesh;
                            instance.transform.GetChild(0).GetComponent<MeshRenderer>().materials = item.materials;
                            instance.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                            instance.transform.localScale = Vector3.one;
                            grabbing = item;
                        }
                    }
                    else
                    {
                        actionsText.text = $"[{interact.action.GetBindingDisplayString()}] Grab";
                    }
                }
            }
            else
            {
                var droppable = hit.transform.GetComponentInParent<IDroppable>();
                if (droppable != null)
                {
                    if (interact.action.WasPressedThisFrame())
                    {
                        if (droppable.Drop(hit.point, grabbing))
                        {
                            grabbing = null;
                            for (int i = transform.childCount - 1; i >= 0; i--)
                            {
                                DestroyImmediate(transform.GetChild(i).gameObject);
                            }
                        }
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
