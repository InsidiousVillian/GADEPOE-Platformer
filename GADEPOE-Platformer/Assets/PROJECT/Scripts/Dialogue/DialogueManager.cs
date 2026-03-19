using System.Collections; // Required for Coroutines
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*purpoe of DialogueManagaer script
    - tracks converdsation state (active/inactive)
    - manages dialogue queue (lines to display)
    - handles UI updates (speaker name, dialogue text)
    - implements typewriter effect for dialogue text
 */

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI References")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI dialogueBodyText;

    [Header("Settings")]
    [SerializeField] private float typingSpeed = 0.04f; 

    private Queue<DialogueLine> dialogueQueue;
    public bool IsDialogueActive { get; private set; }
    private bool isTyping; 

    void Awake()
    {
        // Singleton pattern to ensure only one instance of DialogueManager exists
        Instance = this;
        // Initialise the dialogue queue
        dialogueQueue = new Queue<DialogueLine>();
        // Ensure dialogue panel is hidden at start
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }

    public void StartDialogue(DialogueLine[] lines) 
    {
        IsDialogueActive = true;
        dialoguePanel.SetActive(true);
        dialogueQueue.Clear();

        foreach (DialogueLine line in lines)
        {
            dialogueQueue.Enqueue(line);
        }

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        //if currentrly typing doenst allow skippinh to next line 
        if (isTyping) return; 

        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = dialogueQueue.Dequeue();
        speakerText.text = currentLine.speaker;
        
        // Start the typewriter effect
        StopAllCoroutines();
        StartCoroutine(TypeLine(currentLine.text));
    }

    //using Enumerator to wait for typing speed and then go to next line 
    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueBodyText.text = ""; 

        foreach (char letter in line.ToCharArray())
        {
            dialogueBodyText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    private void EndDialogue()
    {
        isTyping = false;
        IsDialogueActive = false;
        dialoguePanel.SetActive(false);
    }
}