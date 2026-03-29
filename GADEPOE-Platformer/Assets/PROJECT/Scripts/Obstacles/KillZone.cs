using UnityEngine;

public class KillZone : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        gameManager.RespawnPlayer();
        Debug.Log("Player hit kill zone! Respawning");
    }
}
