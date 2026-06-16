using UnityEngine;

public class SpeedyGoon : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        // Define unique baseline stats for the fast variant
        enemyName = "Speedy Goon";
        health = 50f;
        movementSpeed = 6.0f; // Fast!
    }
}