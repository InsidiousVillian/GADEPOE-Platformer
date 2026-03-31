using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    public int currentHealth;

    [Header("Economy")]
    public int goldCount = 0;

    // UI Events to notify the UIManager of changes
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


    // This allows Collectables to call this method
    public void AddScore(int amount)
    {
        goldCount += amount;
        OnGoldChanged?.Invoke(goldCount);
        Debug.Log("Gold Collected! Total: " + goldCount);
    }
}