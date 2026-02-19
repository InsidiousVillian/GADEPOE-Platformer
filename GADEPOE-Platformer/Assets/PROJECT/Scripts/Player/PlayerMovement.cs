using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    

    [Header("Variables")]
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float forwardForce = 5f;
    

    [Header("GameObjects, Transforms")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector3 facingDirection;
    public Transform cameraTransform;
    private Animator animator;
    private Rigidbody rb;


    [Header("Checks")]
    private bool isGrounded;
    private float FallingTimerCheck = 0.0f;
    private bool canMove = true;
    private float resetTimer = 0.5f;
    private float currentTimer = 0;

    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckGround();

        HandleFacingInput();

        
        Debug.Log(currentTimer);

        if (!canMove)
        {
            currentTimer+= Time.deltaTime;

            if (currentTimer >= resetTimer)
            {
                canMove = true;
                currentTimer = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canMove)
        {
            

            //CheckIfCanMove();

            Jump();

            canMove = false;
            currentTimer = 0f;
            

            
            //canMove = false;

        }

        
       
    }

    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);

        // Kill sliding when grounded
        if (isGrounded)
        {
            rb.linearVelocity = new Vector3(0, 0, 0); //rb.linearVelocity.y 
        }
        else
        {
            FallingTimerCheck += Time.deltaTime;
        }

        if (FallingTimerCheck >= 1)
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
