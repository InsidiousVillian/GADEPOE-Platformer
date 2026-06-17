using UnityEngine;

public class StandardZoneFactory : MonoBehaviour, IEnemyFactory
{
    // Spawns a fast goon and returns the base Enemy component
    public Enemy CreateStandardEnemy(GameObject prefab, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        GameObject enemyObj = Instantiate(prefab, spawnPosition, spawnRotation);
        
        // Ensure the prefab has the concrete script attached
        SpeedyGoon goon = enemyObj.GetComponent<SpeedyGoon>();
        if (goon == null)
        {
            goon = enemyObj.AddComponent<SpeedyGoon>();
        }
        
        return goon;
    }

    // Spawns a heavy brute and returns the base Enemy component
    public Enemy CreateHeavyEnemy(GameObject prefab, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        GameObject enemyObj = Instantiate(prefab, spawnPosition, spawnRotation);
        
        // Ensure the prefab has the concrete script attached
        HeavyBrute brute = enemyObj.GetComponent<HeavyBrute>();
        if (brute == null)
        {
            brute = enemyObj.AddComponent<HeavyBrute>();
        }
        
        return brute;
    }
}