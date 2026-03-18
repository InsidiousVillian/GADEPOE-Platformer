using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; // Singleton for easy access
    private Queue<DialogueLine> dialogueQueue;
    public bool IsDialogueActive { get; private set; }

    void Awake()
    {
        Instance = this;
        dialogueQueue = new Queue<DialogueLine>();
    }

    public void StartDialogue(DialogueData data)
    {
        IsDialogueActive = true;
        dialogueQueue.Clear();

        foreach (DialogueLine line in data.lines)
        {
            dialogueQueue.Enqueue(line);
        }

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = dialogueQueue.Dequeue();
        Debug.Log($"[{currentLine.speaker}]: {currentLine.text}");
    }

    private void EndDialogue()
    {
        Debug.Log("End of conversation.");
        IsDialogueActive = false;
    }
}