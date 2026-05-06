using UnityEngine;

public abstract class AbstractEnemyFactory : MonoBehaviour
{
    // The blueprint for creating any enemy
    public abstract EnemyBase CreateEnemy(string type, Vector3 position);
}