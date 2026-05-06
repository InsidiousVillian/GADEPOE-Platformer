using UnityEngine;

namespace JetKingston.GameLogic // This hides your script from the other one
{
    public class GameManager : MonoBehaviour
    {
        public AIEnemyFactory enemyFactory;
        public Transform[] spawnPoints;

        void Start()
        {
            if (enemyFactory != null)
            {
                enemyFactory.CreateEnemy("FastPatrol", spawnPoints[0].position);
                Debug.Log("Spawned FastPatrol Enemy at " + spawnPoints[0].position);
            }
        }
    }
}