using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 8f;
    public float forwardForce = 5f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckGround();

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
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    void Jump()
    {
        rb.velocity = Vector3.zero;

        Vector3 force = new Vector3(0, jumpForce, forwardForce);
        rb.AddForce(force, ForceMode.Impulse);
    }
}
