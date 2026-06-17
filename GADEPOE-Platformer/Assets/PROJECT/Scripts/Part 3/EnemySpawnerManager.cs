using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject speedyGoonPrefab;
    public GameObject heavyBrutePrefab;

    [Header("Spawn Configuration")]
    public GraphNode startingWaypoint;
    public Transform spawnLocation;

    private IEnemyFactory enemyFactory;

    private void Start()
    {
        // Grabs the concrete factory attached to this GameObject
        enemyFactory = GetComponent<IEnemyFactory>();

        if (enemyFactory == null)
        {
            Debug.LogError("No Enemy Factory component found on this Spawner Manager!");
            return;
        }

        SpawnWave();
    }

    private void SpawnWave()
    {
        if (startingWaypoint == null || spawnLocation == null) return;

        // 1. Spawn a Speedy Goon via the factory interface
        Enemy fastEnemy = enemyFactory.CreateStandardEnemy(speedyGoonPrefab, spawnLocation.position, spawnLocation.rotation);
        // Inject the starting pathnode to kick off the custom Graph pathfinding!
        fastEnemy.Initialize(startingWaypoint);

    }
}