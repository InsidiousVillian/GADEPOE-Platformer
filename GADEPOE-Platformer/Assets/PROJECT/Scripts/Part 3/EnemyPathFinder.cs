using UnityEngine;

public class EnemyPathfinder : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3.0f;
    public float arrivalThreshold = 0.2f;

    [Header("Graph Target")]
    public GraphNode currentTargetNode;

    private void Update()
    {
        if (currentTargetNode == null) return;

        // Move towards the current target node
        MoveTowardsTarget();

        // Check if the enemy has arrived at the node
        if (Vector3.Distance(transform.position, currentTargetNode.transform.position) < arrivalThreshold)
        {
            SelectNextNode();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (currentTargetNode.transform.position - transform.position).normalized;
        
        // Simple forward movement (we will adapt this for NavMesh/Moving Platforms next)
        transform.position += direction * speed * Time.deltaTime;

        // Rotate to face the direction of movement
        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }
    }

    private void SelectNextNode()
    {
        // Safety check: Make sure the node actually has neighbors to branch to
        if (currentTargetNode.neighbors.Count > 0)
        {
            // Randomly select an index from the list of neighbors (left, right, or straight)
            int randomIndex = Random.Range(0, currentTargetNode.neighbors.Count);
            currentTargetNode = currentTargetNode.neighbors[randomIndex];
        }
        else
        {
            // If it's a dead end, stop or handle turning around
            currentTargetNode = null; 
        }
    }
}