using UnityEngine;

public class WaypointSetup : MonoBehaviour
{
    public Transform[] waypointsInOrder; 
    
    private WaypointList list;

    void Awake()
    {
        // Get the reference to your Custom LinkedList script
        list = GetComponent<WaypointList>();

        if (waypointsInOrder.Length > 0 && list != null)
        {
            // Loop through the array and add each transform to your custom list
            foreach (Transform wp in waypointsInOrder)
            {
                list.AddWaypoint(wp);
            }
            
            Debug.Log("Custom LinkedList populated with " + waypointsInOrder.Length + " waypoints.");
            // Find all agents in the scene and tell them the list is ready
            AIPatrolAgent[] agents = FindObjectsOfType<AIPatrolAgent>();
            foreach(AIPatrolAgent agent in agents) {
                agent.InitializePatrol();
            }
        }
    }
}
