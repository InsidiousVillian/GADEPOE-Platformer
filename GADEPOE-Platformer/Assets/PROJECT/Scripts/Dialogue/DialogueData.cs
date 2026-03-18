using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speaker;
    public string text;
}

[System.Serializable]
public class DialogueData
{
    public DialogueLine[] lines;
}