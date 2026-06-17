using UnityEngine;

// An interface allows the player trigger script to easily talk to this platform
public interface IMovingPlatform
{
    void StartMoving();
}

public class SimpleMovingPlatform : MonoBehaviour, IMovingPlatform
{
    [Header("Movement Positions")]
    public Transform startPoint;
    public Transform endPoint;
    
    [Header("Settings")]
    public float speed = 2.0f;
    public bool isMoving = true; 

    private Vector3 currentTarget;

    private void Start()
    {
        if (startPoint != null)
        {
            transform.position = startPoint.position;
            currentTarget = endPoint.position;
        }
    }

    private void Update()
    {
        // Only move if activated
        if (!isMoving || startPoint == null || endPoint == null) return;

        // Move the platform toward the target position
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        // If the platform reaches the target, swap destinations to loop back and forth
        if (Vector3.Distance(transform.position, currentTarget) < 0.01f)
        {
            currentTarget = (currentTarget == startPoint.position) ? endPoint.position : startPoint.position;
        }
    }

    public void StartMoving()
    {
        isMoving = true;
    }
}