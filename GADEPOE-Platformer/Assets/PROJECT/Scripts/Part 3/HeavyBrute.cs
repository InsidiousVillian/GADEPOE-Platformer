using UnityEngine;

public class HeavyBrute : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        // Define unique baseline stats for the slow/tank variant
        enemyName = "Heavy Brute";
        health = 200f;
        movementSpeed = 2.0f; // Slow but tough
    }
}