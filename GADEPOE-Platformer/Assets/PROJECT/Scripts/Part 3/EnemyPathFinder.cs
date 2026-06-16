using UnityEngine;
using UnityEngine.AI; 

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyPathfinder : MonoBehaviour
{
    [Header("Graph Target")]
    public GraphNode currentTargetNode;
    
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        if (currentTargetNode == null) return;

        // Check if the agent has reached its destination
        // remainingDistance <= stoppingDistance means it has arrived at the node
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            SelectNextNode();
        }
    }

    private void SelectNextNode()
    {
        if (currentTargetNode.neighbors.Count > 0)
        {
            int randomIndex = Random.Range(0, currentTargetNode.neighbors.Count);
            currentTargetNode = currentTargetNode.neighbors[randomIndex];
            
            // Tell the NavMeshAgent to calculate a path to the new node
            UpdateAgentDestination();
        }
        else
        {
            currentTargetNode = null; 
        }
    }

    private void UpdateAgentDestination()
    {
        if (currentTargetNode != null && agent != null)
        {
            agent.SetDestination(currentTargetNode.transform.position);
        }
    }
}