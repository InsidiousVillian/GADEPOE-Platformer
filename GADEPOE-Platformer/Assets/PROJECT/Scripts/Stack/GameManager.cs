using UnityEngine;

public class GameManager : MonoBehaviour
{
    //custom stack - dont know why my stack script name is different - did try to change script name
    private Stack myCustomStack; 


    public GameObject player; 
    
    //reference to playerstats
    public PlayerStats playerStats;

    void Awake()
    {
        // starts the stack on wake
        myCustomStack = new Stack();
    }

    void Start()
    {
        //checks if i assigned player and player stats
        if (player != null && playerStats != null)
        {
            // Create the starting checkpointy
            CheckpointData startData = new CheckpointData(
                player.transform.position, 
                100, 
                0    
            );
            
            //pushes starting checkpoint onto stack
            myCustomStack.Push(startData);
            Debug.Log("Starting Checkpoint Saved!");
        }
    }

    public void OnPlayerHitCheckpoint(Transform newPosition)
    {
        myCustomStack.Pop(); //pops the last checkpoint off the stack

        CheckpointData newCheckpoint = new CheckpointData(
            newPosition.position, 
            playerStats.currentHealth, 
            playerStats.goldCount
        );
        myCustomStack.Push(newCheckpoint);
        Debug.Log("New Checkpoint Saved!");

    }

    public void RespawnPlayer()
    {
        CheckpointData LastCheckpoint = myCustomStack.Peek(); 

        if (LastCheckpoint != null)
        {
            player.transform.position = LastCheckpoint.playerPosition; 
            playerStats.currentHealth = LastCheckpoint.playerLives; 
            playerStats.goldCount = LastCheckpoint.playerGold; 
            Debug.Log("Player Respawned at Last Checkpoint!");
        }
        else
        {
            Debug.LogWarning("No checkpoints available! Cannot respawn player.");
        }

    }
}