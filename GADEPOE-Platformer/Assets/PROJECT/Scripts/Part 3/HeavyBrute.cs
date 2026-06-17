using UnityEngine;

public class HeavyBrute : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        enemyName = "Heavy Brute";
        health = 200f;
        movementSpeed = 2.0f; 
    }
}