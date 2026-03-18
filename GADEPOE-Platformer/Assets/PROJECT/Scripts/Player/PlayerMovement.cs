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
        CheckGround();
        HandleInput();
        HandleRotation();
        HandleAnimations();

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

        Vector3 velocityChange = (targetVelocity - new Vector3(velocity.x, 0, velocity.z))
                                 * acceleration * control * Time.fixedDeltaTime;

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
}