using UnityEngine;

public class Collectables : MonoBehaviour
{
    public enum Type { GoldBar, HealthPack, PowerUp, None }
    [SerializeField] private Type collectableType;
    [SerializeField] private int value = 10;

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
                // Calls the method that triggers the Gold UI update
                stats.AddScore(value); 
                break;

            case Type.HealthPack:
                // CHANGED: Instead of stats.lives += value, we call Heal()
                // This ensures the UI Slider moves!
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
}