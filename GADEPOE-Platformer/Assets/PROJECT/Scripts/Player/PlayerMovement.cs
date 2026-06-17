using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float acceleration = 20f;
    [SerializeField] float airControl = 0.5f;

    [Header("Jump")]
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float gravityMultiplier = 2.5f;

    [Header("Ground Check")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckDistance = 1.1f;

    [Header("References")]
    public Transform cameraTransform;

    [Header("Footstep SFX Configurations")]
    [SerializeField] private float stepCooldown = 0.4f; // Time in seconds between step loops
    private float stepTimer = 0f;
 
    private Rigidbody rb;
    private Animator animator;

    private Vector3 moveDirection;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        rb.freezeRotation = true;
    }

    void Update()
    {
        // Stops player movement and jumping when dialogue is active
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueActive) 
        { 
            rb.linearVelocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            return; 
        }

        CheckGround();
        HandleInput();
        HandleRotation();
        HandleAnimations();
        HandleFootstepAudio(); // Process our custom HashMap audio tick window safely

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Move();
        ApplyExtraGravity();
    }

    void HandleInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

    
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        if (cameraTransform == null) return;

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        moveDirection = (camForward * v + camRight * h).normalized;
    }

    void Move()
    {
        float control = isGrounded ? 1f : airControl;

        Vector3 targetVelocity = moveDirection * moveSpeed;
        Vector3 velocity = rb.linearVelocity;

        Vector3 velocityChange = (targetVelocity - new Vector3(velocity.x, 0, velocity.z)) * acceleration * control * Time.fixedDeltaTime;
        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    void HandleRotation()
    {
        if (moveDirection == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        animator.SetTrigger("Jump");
    }

    // Applies extra gravity when in the air for a snappier jump feel
    void ApplyExtraGravity()
    {
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
        }
    }

    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    void HandleAnimations()
    {
        float speed = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;

        if (speed < 0.05f) { speed = 0f; }

        animator.SetFloat("Speed", speed);
        animator.SetBool("isGrounded", isGrounded);
    }

    // Process rhythm calculations and request audio assets from our custom ADT structure
    void HandleFootstepAudio()
    {
        // Calculate the actual horizontal footprint speed velocity 
        float horizontalSpeed = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z).magnitude;

        // Condition check: Character must be actively traversing the ground surfaces and inputting direction
        if (isGrounded && horizontalSpeed > 0.1f && moveDirection.magnitude > 0.1f)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepCooldown)
            {
                // Query our custom Separate-Chaining HashMap structure via the global manager component instance
                SFXManager sfx = FindObjectOfType<SFXManager>();
                if (sfx != null)
                {
                    sfx.PlaySFX("Footstep");
                }

                stepTimer = 0f; // Reset loop state window execution window
            }
        }
        else
        {
            // Instantly prime the timer context when stationary so execution triggers on the very first physical stride step
            stepTimer = stepCooldown;
        }
    }
}