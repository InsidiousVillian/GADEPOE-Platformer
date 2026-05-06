using UnityEngine;

public class AIEnemyFactory : AbstractEnemyFactory
{
    [SerializeField] private GameObject patrolPrefab;
    public override EnemyBase CreateEnemy(string type, Vector3 position)
    {
        GameObject newEnemyObj;
        EnemyBase enemyScript;

        if (type == "FastPatrol")
        {
            newEnemyObj = Instantiate(patrolPrefab, position, Quaternion.identity);
            enemyScript = newEnemyObj.GetComponent<PatrolEnemy>();
            enemyScript.Speed = 10f;
            enemyScript.Size = new Vector3(0.5f, 0.5f, 0.5f);
            enemyScript.Appearance = Color.green;
        }
        else
        {
            return null;
        }

        enemyScript.Initialize();
        return enemyScript;
    }
}