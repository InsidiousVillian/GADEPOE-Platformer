using UnityEngine;

public interface IEnemyFactory
{
    // Every factory must implement a method to spawn a standard unit
    Enemy CreateStandardEnemy(GameObject prefab, Vector3 spawnPosition, Quaternion spawnRotation);

    // Every factory must implement a method to spawn an heavy unit
    Enemy CreateHeavyEnemy(GameObject prefab, Vector3 spawnPosition, Quaternion spawnRotation);
}
