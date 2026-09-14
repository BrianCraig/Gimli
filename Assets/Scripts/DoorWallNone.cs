using UnityEngine;

public enum DoorWallNoneState
{
    Door,
    Wall,
    None
}


[RequireComponent(typeof(VisibleTrigger))]
public class DoorWallNone : MonoBehaviour
{
    public GameObject door;
    public GameObject wall;
    [SerializeField] DoorWallNoneState state;
    public DoorWallNoneState State
    {
        get => state;
        set
        {
            if (state == value) return;
            state = value;
            UpdateChildInstances();
        }
    }
    private VisibleTrigger trigger;

    void Start()
    {
        trigger = GetComponent<VisibleTrigger>();
    }

    void OnValidate()
    {
        // Since unity does not call the state setter on the editor
        // We manually call it on attribute change trough the inspector
        UpdateChildInstances();
    }

    void UpdateChildInstances()
    {
        door.SetActive(state == DoorWallNoneState.Door);
        wall.SetActive(state == DoorWallNoneState.Wall);
    }
    void Update()
    {

    }
}
