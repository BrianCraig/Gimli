using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform cameraRepos;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float lookSensitivity = 0.1f;

    InputAction move;
    InputAction look;

    float pitch;

    void Awake()
    {
        var input = GetComponent<PlayerInput>();

        move = input.actions["Move"];
        look = input.actions["Look"];

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Look();
        Move();
    }

    void Look()
    {
        Vector2 delta = look.ReadValue<Vector2>();

        // Yaw: rotate the whole player.
        transform.Rotate(Vector3.up, delta.x * lookSensitivity);

        // Pitch: rotate only the camera pivot.
        pitch -= delta.y * lookSensitivity;
        pitch = Mathf.Clamp(pitch, -89f, 89f);

        cameraRepos.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void Move()
    {
        Vector2 input = move.ReadValue<Vector2>();

        // Camera's forward projected onto the ground plane.
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = Camera.main.transform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 direction = forward * input.y + right * input.x;

        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}