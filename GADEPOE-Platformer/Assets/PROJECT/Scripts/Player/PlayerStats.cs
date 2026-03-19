using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Current Stats")]
    public int score = 0;
    public int lives = 3;

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"Score Updated: {score}");
    }

    public void LoseHealth()
    {
        lives--;
        Debug.Log($"Life Lost! Remaining: {lives}");
        
        if (lives <= 0)
        {
            Debug.Log("Game Over screen overlay");
        }
    }
}