using UnityEngine;

public class WaypointList : MonoBehaviour
{
    private WaypointNode head; // The first waypoint in the list.
    private WaypointNode tail; // The last waypoint in the list.

    // Method to add a new waypoint to our custom structure.
    public void AddWaypoint(Transform newTransform)
    {
        WaypointNode newNode = new WaypointNode(newTransform);

        if (head == null)
        {
            // If the list is empty, this node is both head and tail.
            head = newNode;
            tail = newNode;
        }
        else
        {
            // Link the old tail to the new node.
            tail.Next = newNode;
            // Update the tail reference.
            tail = newNode;
        }

        //Point the tail back to the head to make it circular.
        // This allows complete loops as required.
        tail.Next = head;
    }

    // A helper method for the AI to get the starting point.
    public WaypointNode GetFirstNode()
    {
        return head;
    }
}
