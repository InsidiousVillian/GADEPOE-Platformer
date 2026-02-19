using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    

    [Header("Variables")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float rotationSpeed = 12f;
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float forwardForce = 5f;

    [Header("GameObjects, Transforms")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector3 facingDirection;
    public Transform cameraTransform;
    private Animator animator;
    private Rigidbody rb;
    private bool isGrounded;

    private float timer = 0;

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
        else
        {
            timer += Time.deltaTime;
        }

        if (timer >= 1)
        {
            animator.SetTrigger("isFalling");
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
        facingDirection = Vector3.forward;   // snaps to +z

        if (Input.GetKeyDown(KeyCode.S))
        facingDirection = Vector3.back;      // snaps to -z

        if (Input.GetKeyDown(KeyCode.D))
        facingDirection = Vector3.right;     // snaps to +x

        if (Input.GetKeyDown(KeyCode.A))
        facingDirection = Vector3.left;      // snaps to -x

        // rotate player to face direction
        if (facingDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(facingDirection);
            transform.rotation = targetRotation;
        }
    }

}
