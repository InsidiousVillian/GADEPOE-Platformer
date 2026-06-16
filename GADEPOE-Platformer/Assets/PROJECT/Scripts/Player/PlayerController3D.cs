using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController3D : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 7.0f;
    [SerializeField] private float rotationSpeed = 15.0f;

    [Header("Gravity & Physics")]
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private float jumpForce = 8.0f;

    private CharacterController controller;
    private Camera mainCamera;
    private Vector3 moveDirection = Vector3.zero;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main; // Automatically grabs the camera with the CinemachineBrain

        if (mainCamera == null)
        {
            mainCamera = FindObjectOfType<Camera>();
        
            if (mainCamera == null)
            {
                Debug.LogError("PlayerController3D: There is literally no Camera in your scene! Please add one.");
            }
            else
            {
                Debug.LogWarning("PlayerController3D: Found a camera, but it wasn't tagged 'MainCamera'. Please tag it!");
            }
        }

    }

    private void Update()
    {
        // 1. Get raw input from WASD / Arrow Keys
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (controller.isGrounded)
        {
            // 2. Map input relative to the ACTIVE camera's view matrix
            if (inputDirection.magnitude >= 0.1f)
            {
                // Grab the current camera's orientation vectors
                Vector3 camForward = mainCamera.transform.forward;
                Vector3 camRight = mainCamera.transform.right;
                
                // CRUCIAL STEP: Flatten the vectors on the Y axis. 
                // This prevents the player from forcing themselves downward or upward if the camera tilts.
                camForward.y = 0f;
                camRight.y = 0f;
                
                // Re-normalize to keep movement speeds perfectly uniform
                camForward.Normalize();
                camRight.Normalize();

                // Compute the relative movement vector
                // W/S controls movement along the camera's forward line, A/D along its right line
                Vector3 targetDirection = (camForward * inputDirection.z) + (camRight * inputDirection.x);
                targetDirection.Normalize();

                // Assign speed velocity
                moveDirection = targetDirection * moveSpeed;

                // 3. Face the running direction instantly
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                // Smoothly stop horizontal movement if no buttons are pressed
                moveDirection.x = 0f;
                moveDirection.z = 0f;
            }

            // 4. Handle Jumping
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }

        // 5. Apply Gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // 6. Execute Movement via the character capsule
        controller.Move(moveDirection * Time.deltaTime);
    }
}