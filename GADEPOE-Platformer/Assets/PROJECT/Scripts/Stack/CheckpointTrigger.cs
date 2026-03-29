using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.OnPlayerHitCheckpoint(this.transform);
            Debug.Log("Player hit checkpoint! Saving progress.");
        }
    }
}
