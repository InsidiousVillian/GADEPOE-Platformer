using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : EnemyBase
{
    public WaypointList waypointList; 
    private WaypointNode currentNode;
    private NavMeshAgent navAgent;

    public override void Initialize()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = this.Speed;
        
        // Apply the visual variations (Size and Color) from the Base Class
        ApplyVisuals();

        // Start the patrol logic
        if (waypointList != null)
        {
            currentNode = waypointList.GetFirstNode();
            SetNextDestination();
        }
    }

    private void SetNextDestination()
    {
        if (currentNode != null)
        {
            navAgent.SetDestination(currentNode.Waypoint.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Logic to move to the next node in the LinkedList
        if (currentNode != null && other.transform == currentNode.Waypoint)
        {
            currentNode = currentNode.Next;
            SetNextDestination();
        }

        // Kill logic: If hitting the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hero Killed by Patrolling Enemy!");
            // Add your game-over logic here
        }
    }
}