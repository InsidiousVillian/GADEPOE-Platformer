using UnityEngine;

public class HashNode
{
    public string Key { get; set; }
    public AudioClip Value { get; set; }
    public HashNode Next { get; set; }

    public HashNode(string key, AudioClip value)
    {
        this.Key = key;
        this.Value = value;
        this.Next = null;
    }
}