using UnityEngine;

public class CheckpointData
{
    public Vector3 playerPosition;
    public int playerLives;
    public int playerGold;

    public CheckpointData(Vector3 position, int Lives, int gold)
    {
        playerPosition = position;
        playerGold = gold;
        playerLives = Lives;
    }
}
