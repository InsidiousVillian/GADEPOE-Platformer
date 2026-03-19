using System.IO;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [Header("File Settings")]
    [SerializeField] private string fileName = "dialogue.json"; 

    [Header("UI Prompt")]
    [Tooltip("Drag the 'E' Prompt Canvas/Object here")]
    [SerializeField] private GameObject interactionPrompt; 

    private bool playerInRange;

    void Awake()
    {
        // Ensure the prompt is hidden when the game starts
        if (interactionPrompt != null) interactionPrompt.SetActive(false);
    }

    void Update()
    {
        // Start or Cycle dialogue
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!DialogueManager.Instance.IsDialogueActive)
            {
                LoadAndTriggerDialogue();
                // Hide prompt while talking so it doesn't clutter the screen
                if (interactionPrompt != null) interactionPrompt.SetActive(false);
            }
            else
            {
                DialogueManager.Instance.DisplayNextLine();
            }
        }

        // Re-show prompt if dialogue ends but player is still in range
        if (playerInRange && !DialogueManager.Instance.IsDialogueActive)
        {
            if (interactionPrompt != null && !interactionPrompt.activeSelf)
            {
                interactionPrompt.SetActive(true);
            }
        }
    }

    private void LoadAndTriggerDialogue()
    {
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
            if (interactionPrompt != null) interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (interactionPrompt != null) interactionPrompt.SetActive(false);
        }
    }
}