using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("Economy")]
    private int goldCount = 0;

    // UI Events - These "shout" to the UIManager
    public event Action<int, int> OnHealthChanged;
    public event Action<int> OnGoldChanged;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        currentHealth = maxHealth;
    }

    private void Start()
    {
        // Initial UI Sync
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        OnGoldChanged?.Invoke(goldCount);
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        Debug.Log("Healed! Current Health: " + currentHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    // Fixes your CS1061 Error: 
    // This allows Collectables.cs to call "AddScore"
    public void AddScore(int amount)
    {
        goldCount += amount;
        OnGoldChanged?.Invoke(goldCount);
        Debug.Log("Gold Collected! Total: " + goldCount);
    }
}