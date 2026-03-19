using System.IO;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [Tooltip("The exact name of the file in StreamingAssets (including .json)")]
    [SerializeField] private string fileName = "dialogue.json"; 
    [Tooltip("The ID in the JSON file for this specific NPC/Object")]
    [SerializeField] private string conversationID; 

    [Header("UI Prompt")]
    [Tooltip("Drag the floating 'E' Canvas/Object here")]
    [SerializeField] private GameObject interactionPrompt; 

    private bool playerInRange;

    void Awake()
    {
        // Ensure the prompt is hidden when the game starts
        if (interactionPrompt != null) interactionPrompt.SetActive(false);
    }

    void Update()
    {
        // Check for interaction input
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // If dialogue isn't running, start it
            if (!DialogueManager.Instance.IsDialogueActive)
            {
                LoadAndTriggerDialogue();
                
                // Hide the 'E' prompt while the dialogue is open
                if (interactionPrompt != null) interactionPrompt.SetActive(false);
            }
            else
            {
                // If dialogue IS running, E will cycle to the next line
                DialogueManager.Instance.DisplayNextLine();
            }
        }

        // Optional: Re-show prompt if dialogue ended but player is still standing there
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
        // Path to your StreamingAssets folder
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string jsonContents = File.ReadAllText(filePath);
            
            // Parse the JSON into our DialogueData object
            DialogueData data = JsonUtility.FromJson<DialogueData>(jsonContents);

            // Find the specific conversation in the array that matches our ID
            Conversation myConvo = System.Array.Find(data.conversations, c => c.id == conversationID);

            if (myConvo != null)
            {
                // Pass only the lines from that specific conversation to the Manager
                DialogueManager.Instance.StartDialogue(myConvo.lines);
            }
            else
            {
                Debug.LogError($"Conversation ID '{conversationID}' not found in {fileName} on {gameObject.name}");
            }
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
            Debug.Log($"In range of {gameObject.name}. Press E to interact.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (interactionPrompt != null) interactionPrompt.SetActive(false);
            
            // Optional: Close dialogue if the player walks away mid-conversation
            // DialogueManager.Instance.EndDialogue(); 
        }
    }
}