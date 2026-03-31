using UnityEngine;
/* 
    This script is attached to a trigger object that the player can collide with.
    When the player hits the checkpoint, it calls the GameManager's method to save the player's progress. 
    The GameManager will then create a CheckpointData object with the player's current position, lives, and gold, and store it for later use when the player respawns.
    The CheckpointData object is then stored in the player's save data, and can be retrieved when the player respawns.

*/ 
public class CheckpointTrigger : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Call the method to save player's progress
            gameManager.OnPlayerHitCheckpoint(this.transform);
            Debug.Log("Player hit checkpoint! Saving progress.");
        }
    }
}
