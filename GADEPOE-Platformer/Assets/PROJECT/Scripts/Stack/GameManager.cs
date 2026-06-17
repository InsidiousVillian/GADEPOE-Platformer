using UnityEngine;

/*
    This GameManager script is responsible for managing the player's checkpoints and respawn system. 
    It uses a custom stack to store checkpoint data, which includes the player's position, health, and gold count. 
    When the player hits a checkpoint trigger, the GameManager saves the current state of the player onto the stack. 
    If the player dies, the GameManager can retrieve the last checkpoint data from the stack and respawn the player at that location with the saved health and gold.
*/
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
        SFXManager sfx = FindObjectOfType<SFXManager>();
        if (sfx != null)
            {
               
                sfx.PlayBackgroundMusic("LevelMusic"); 
        }
    }

    public void OnPlayerHitCheckpoint(Transform newPosition)
    {
        myCustomStack.Pop(); //pops the last checkpoint off the stack

        // Creates a new checkpoint with the player's current position, health, and gold
        CheckpointData newCheckpoint = new CheckpointData(
            newPosition.position, 
            playerStats.currentHealth, 
            playerStats.goldCount
        );
        // Pushes the new checkpoint onto the stack
        myCustomStack.Push(newCheckpoint);
        Debug.Log("New Checkpoint Saved!");

    }

    public void RespawnPlayer()
    {
        // Get the last checkpoint from the stack
        CheckpointData LastCheckpoint = myCustomStack.Peek(); 

        // If there is a checkpoint, respawn the player at that location with the saved health and gold
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