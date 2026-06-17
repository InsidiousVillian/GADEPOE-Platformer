using UnityEngine;

public class SpeedyGoon : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        
        enemyName = "Speedy Goon";
        health = 50f;
        movementSpeed = 6.0f; 
    }
}