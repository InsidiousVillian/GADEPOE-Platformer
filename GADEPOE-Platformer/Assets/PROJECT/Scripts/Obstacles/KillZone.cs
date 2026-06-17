using UnityEngine;

public class KillZone : MonoBehaviour
{
    public GameManager gameManager;

    [Header("Custom HashMap Audio Mapping")]
    [Tooltip("The exact key string registered inside your SFXManager for the player's death sound")]
    [SerializeField] private string deathSFXKey = "Death";

    void OnTriggerEnter(Collider other)
    {
        // Only trigger death if the thing hitting the hazard volume is actually our hero character
        if (other.CompareTag("Player"))
        {
            // 1. Play the death audio cue out of our custom data structure
            PlayDeathSound();

            // 2. Execute your game loop respawn sequence routine
            if (gameManager != null)
            {
                gameManager.RespawnPlayer();
            }
            
            Debug.Log("Player hit kill zone! Respawning");
        }
    }

    private void PlayDeathSound()
    {
        // Find our custom map instance globally and look up the clip asset by its key
        SFXManager sfx = FindObjectOfType<SFXManager>();
        if (sfx != null && !string.IsNullOrEmpty(deathSFXKey))
        {
            sfx.PlaySFX(deathSFXKey);
        }
    }
}