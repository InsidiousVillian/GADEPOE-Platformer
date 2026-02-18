using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform cameraTransform;
    public float moveSpeed = 6f;
    public float rotationSpeed = 12f;
    public float jumpForce = 8f;
    public float forwardForce = 5f;
    public LayerMask groundLayer;
    private Vector3 facingDirection;
    private Animator animator;


    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckGround();

        HandleFacingInput();


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);

        // Kill sliding when grounded
        if (isGrounded)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    void Jump()
    {
        if (facingDirection == Vector3.zero)
        return;

        rb.linearVelocity = Vector3.zero;

        Vector3 force = facingDirection * forwardForce + Vector3.up * jumpForce;
        rb.AddForce(force, ForceMode.Impulse);

        // PLAY ANIMATION
        animator.SetTrigger("Step");
    }

    void HandleFacingInput()
    {
        // 4-direction snap along world axes
        if (Input.GetKeyDown(KeyCode.W))
        facingDirection = Vector3.forward;   // +Z

        if (Input.GetKeyDown(KeyCode.S))
        facingDirection = Vector3.back;      // -Z

        if (Input.GetKeyDown(KeyCode.D))
        facingDirection = Vector3.right;     // +X

        if (Input.GetKeyDown(KeyCode.A))
        facingDirection = Vector3.left;      // -X

        // rotate player to face direction
        if (facingDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(facingDirection);
            transform.rotation = targetRotation;
        }
    }

}
