using UnityEngine;

/// <summary>
/// First-person controller: camera look, movement, and jumping.
/// Attach this script to the Camera object. Assign the Player Body (with CharacterController) in the inspector.
/// </summary>
public class FPSCameraController : MonoBehaviour
{
    [Header("Mouse Settings")]
    [Tooltip("Mouse sensitivity for looking around")]
    public float mouseSensitivity = 100f;

    [Header("Movement Settings")]
    [Tooltip("Walking speed")]
    public float speed = 12f;
    [Tooltip("Jump height")]
    public float jumpHeight = 1.5f;
    [Tooltip("Gravity force (negative)")]
    public float gravity = -9.81f;

    [Header("References")]
    [Tooltip("Reference to the player's body Transform (e.g., the capsule or character root)")]
    public Transform playerBody;

    CharacterController controller;
    float xRotation = 0f;
    Vector3 velocity;

    void Start()
    {
        // Ensure CharacterController on player body
        controller = playerBody.GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("FPSCameraController: No CharacterController found on Player Body.");
        }

        // Lock and hide the cursor at the start
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // ----- Camera Look -----
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Vertical look (pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal look (yaw)
        playerBody.Rotate(Vector3.up * mouseX);

        // ----- Movement & Jump -----
        if (controller != null)
        {
            // Check if grounded
            bool isGrounded = controller.isGrounded;
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // small downward force to stick to ground
            }

            // Read input axes
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            // Movement relative to player orientation
            Vector3 move = playerBody.right * x + playerBody.forward * z;
            controller.Move(move * (speed * Time.deltaTime));

            // Jump
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            // Apply gravity
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    /// <summary>
    /// Optional: Toggle cursor lock state at runtime.
    /// </summary>
    public void ToggleCursorLock()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
