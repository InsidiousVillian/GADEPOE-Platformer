using UnityEngine;

public class WaypointNode
{
    public Transform Waypoint; // The actual position in the scene.
    public WaypointNode Next;  // reference to the next node in the chain.

    // Constructor to initialise the node with a transform.
    public WaypointNode(Transform waypoint)
    {
        this.Waypoint = waypoint;
        this.Next = null;
    }
}
