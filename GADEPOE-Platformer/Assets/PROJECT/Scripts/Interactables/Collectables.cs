using UnityEngine;

public class Collectables : MonoBehaviour
{
    public enum Type { GoldBar, HealthPack, PowerUp, None }
    [SerializeField] private Type collectableType;
    [SerializeField] private int value = 10;

    [Header("Custom HashMap Audio Mapping Keys")]
    [Tooltip("The SFX key used for this specific item type in your SFXManager setup")]
    [SerializeField] private string goldSFXKey = "GoldPickup";
    [SerializeField] private string healthSFXKey = "HealthPickup";
    [SerializeField] private string powerupSFXKey = "PowerUpPickup";

    private float rotationSpeed = 50f;

    void Update()
    {
        // Rotates the collectable for visual effect
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyEffect(other.gameObject);
            
           
            PlayPickupSound();

            Debug.Log($"Collected {collectableType}!");
            Destroy(gameObject);
        }
    }

    private void ApplyEffect(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        if (stats == null)
        {
            Debug.LogError("PlayerStats component not found on player!");
            return;
        }

        switch (collectableType)
        {
            case Type.GoldBar:
                stats.AddScore(value); 
                break;

            case Type.HealthPack:
                stats.Heal(value); 
                break;

            case Type.PowerUp:
                Debug.Log("Power-Up collected! Effect not implemented yet.");
                break;

            case Type.None: 
                Debug.LogWarning("Collectable type is set to None. No effect applied.");
                break;
        }
    }

    // Safely reads your custom polynomial hashmap structure to serve the correct sound file asset
    private void PlayPickupSound()
    {
        SFXManager sfx = FindObjectOfType<SFXManager>();
        if (sfx == null) return;

        string targetKey = string.Empty;

        // Switch to find the correct string key match based on the item type enum properties
        switch (collectableType)
        {
            case Type.GoldBar:
                targetKey = goldSFXKey;
                break;
            case Type.HealthPack:
                targetKey = healthSFXKey;
                break;
            case Type.PowerUp:
                targetKey = powerupSFXKey;
                break;
        }

        if (!string.IsNullOrEmpty(targetKey))
        {
            sfx.PlaySFX(targetKey);
        }
    }
}