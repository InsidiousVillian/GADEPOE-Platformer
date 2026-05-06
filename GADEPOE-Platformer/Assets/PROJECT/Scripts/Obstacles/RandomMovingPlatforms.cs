using UnityEngine;

public class RandomMovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform[] points; 
    public float speed = 3.0f;
    public float waitTime = 1.0f;

    private Vector3 targetPosition;
    private bool isWaiting = false;
    private bool hasInitialized = false;

    void Start()
    {
        // Only start if we have at least 2 points to move between
        if (points != null && points.Length >= 2)
        {
            // Instantly snap to the first point so it doesn't "slide" there from (0,0,0)
            transform.position = points[0].position;
            
            // Pick the NEXT target immediately
            SetRandomTarget();
            hasInitialized = true;
        }
        else
        {
            Debug.LogWarning("Please assign at least 2 points to the RandomMovingPlatform on " + gameObject.name);
        }
    }

    void Update()
    {
        // Don't move if we aren't ready, are waiting, or have no points
        if (!hasInitialized || isWaiting) return;

        // Move strictly toward the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Arrival Check
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition; // Snap for precision
            StartCoroutine(WaitAndMove());
        }
    }

    void SetRandomTarget()
    {
        // Pick a random index from the array
        int randomIndex = Random.Range(0, points.Length);
        targetPosition = points[randomIndex].position;
    }

    System.Collections.IEnumerator WaitAndMove()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        SetRandomTarget();
        isWaiting = false;
    }

    // --- STICKY PLAYER LOGIC ---
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}