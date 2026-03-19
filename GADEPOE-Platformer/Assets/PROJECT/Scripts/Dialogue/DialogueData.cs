[System.Serializable]
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