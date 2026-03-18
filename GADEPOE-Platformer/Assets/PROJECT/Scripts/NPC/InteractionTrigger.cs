using System.IO;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [Header("File Settings")]
    [Tooltip("The exact name of the file in StreamingAssets (including .json)")]
    [SerializeField] private string fileName = "dialogue.json"; 

    private bool playerInRange;

    void Update()
    {
        // Start dialogue
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!DialogueManager.Instance.IsDialogueActive)
            {
                LoadAndTriggerDialogue();
            }
        }

        // Cycle dialogue
        if (DialogueManager.Instance.IsDialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.Instance.DisplayNextLine();
        }
    }

    private void LoadAndTriggerDialogue()
    {
        // This finds the path to your StreamingAssets folder regardless of platform
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string jsonContents = File.ReadAllText(filePath);
            DialogueData data = JsonUtility.FromJson<DialogueData>(jsonContents);
            DialogueManager.Instance.StartDialogue(data);
        }
        else
        {
            Debug.LogError($"Dialogue file not found at: {filePath}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Press E to interact");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }
}