using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyPathfinder))]
public abstract class Enemy : MonoBehaviour
{
    [Header("Base Stats")]
    public string enemyName;
    public float health;
    public float movementSpeed;

    protected NavMeshAgent agent;
    protected EnemyPathfinder pathfinder;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        pathfinder = GetComponent<EnemyPathfinder>();
    }

    // This initialization function allows our Factory to inject unique stats on spawn
    public virtual void Initialize(GraphNode startingNode)
    {
        if (agent != null)
        {
            agent.speed = movementSpeed;
        }
        
        if (pathfinder != null)
        {
            pathfinder.currentTargetNode = startingNode;
        }
    }
}