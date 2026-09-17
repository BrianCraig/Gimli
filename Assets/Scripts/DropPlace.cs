using UnityEngine;

public class DropItem : MonoBehaviour, IDroppable
{
    public bool CanDrop()
    {
        return true;
    }

    public void Drop(Transform incoming_transform, Vector3 _)
    {
        incoming_transform.SetParent(transform);
        incoming_transform.SetLocalPositionAndRotation(Vector3.zero, new Quaternion(-0.181592122f, -0.144707561f, 0.0379015729f, 0.971929789f));
        incoming_transform.localScale = new Vector3(0.460830003f,0.460830003f,0.460830003f);
    }
}
