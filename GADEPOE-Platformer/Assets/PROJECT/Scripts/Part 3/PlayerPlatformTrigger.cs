using UnityEngine;

public class PlayerPlatformTrigger : MonoBehaviour
{
    [Header("Platform to Activate")]
    public SimpleMovingPlatform movingPlatform; 

    private void OnTriggerEnter(Collider other)
    {
        
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