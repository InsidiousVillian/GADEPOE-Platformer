using UnityEngine;
using UnityEngine.AI; 

public class AIPatrolAgent : MonoBehaviour
{
    public WaypointList waypointList; 
    private WaypointNode currentNode;// The node we are currently targeting
    private NavMeshAgent navAgent;       

    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }
    // Logic for when the ai hits the Waypoint trigger.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object we collided with is our current target waypoint.
        if (currentNode != null && other.transform == currentNode.Waypoint)
        {
            // Move to the next node in our custom LinkedList.
            currentNode = currentNode.Next;
            SetDestinationToCurrentNode();
        }
    }

    void SetDestinationToCurrentNode()
    {
        if (currentNode != null)
        {
            navAgent.SetDestination(currentNode.Waypoint.position);
        }
    }

    public void InitializePatrol()
    {
        if (waypointList != null)
        {
            currentNode = waypointList.GetFirstNode();
            if (currentNode != null)
            {
                Debug.Log("Agent initialized. Heading to: " + currentNode.Waypoint.name);
                SetDestinationToCurrentNode();
            }
        }
    }
}

