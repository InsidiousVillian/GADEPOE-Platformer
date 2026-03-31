[System.Serializable]

// This class represents a single line of dialogue, including the speaker and the text.
// The DialogueData class contains an array of conversations, where each conversation has an ID and an array of dialogue lines.
public class DialogueLine
{
    public string speaker;
    public string text;
}

[System.Serializable]
public class Conversation
{
    public string id; 
    public DialogueLine[] lines;
}

[System.Serializable]
public class DialogueData
{
    public Conversation[] conversations; 
}