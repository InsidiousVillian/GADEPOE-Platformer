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
        // Grab the concrete factory attached to this GameObject
        enemyFactory = GetComponent<IEnemyFactory>();

        if (enemyFactory == null)
        {
            Debug.LogError("No Enemy Factory component found on this Spawner Manager!");
            return;
        }

        // Test Spawning! Let's spawn one of each type onto our Graph ADT path
        SpawnWave();
    }

    private void SpawnWave()
    {
        if (startingWaypoint == null || spawnLocation == null) return;

        // 1. Spawn a Speedy Goon via the factory interface
        Enemy fastEnemy = enemyFactory.CreateStandardEnemy(speedyGoonPrefab, spawnLocation.position, spawnLocation.rotation);
        // Inject the starting pathnode to kick off the custom Graph pathfinding!
        fastEnemy.Initialize(startingWaypoint);

        // 2. Spawn a Heavy Brute slightly offset so they don't overlap
        Vector3 offsetPos = spawnLocation.position + new Vector3(2f, 0f, 0f);
        Enemy heavyEnemy = enemyFactory.CreateHeavyEnemy(heavyBrutePrefab, offsetPos, spawnLocation.rotation);
        heavyEnemy.Initialize(startingWaypoint);
    }
}