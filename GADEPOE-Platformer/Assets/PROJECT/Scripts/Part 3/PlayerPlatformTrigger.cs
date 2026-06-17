using UnityEngine;

public class PlayerPlatformTrigger : MonoBehaviour
{
    [Header("Platform to Activate")]
    // Drag your moving platform GameObject here in the Inspector
    public SimpleMovingPlatform movingPlatform; 

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the zone is tagged "Player"
        if (other.CompareTag("Player"))
        {
            if (movingPlatform != null)
            {
                
                movingPlatform.StartMoving();
                Debug.Log("Hero detected! Platform starting...");

                SFXManager sfx = FindObjectOfType<SFXManager>();
                if (sfx != null)
                {
                    sfx.PlaySFX("PlatformStart");
                }
            }
        }
    }
}