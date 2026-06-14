using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    // The list of waypoints this specific node can connect to
    public List<GraphNode> neighbors = new List<GraphNode>();

    // Helper method to easily link nodes together in code or editor
    public void AddNeighbor(GraphNode neighbor)
    {
        if (!neighbors.Contains(neighbor))
        {
            neighbors.Add(neighbor);
        }
    }

    // Visualizes the branching paths in the Unity Editor scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 0.3f);

        if (neighbors == null) return;

        Gizmos.color = Color.blue;
        foreach (GraphNode neighbor in neighbors)
        {
            if (neighbor != null)
            {
                // Draws a line pointing from this node to its neighbor
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
            }
        }
    }
}