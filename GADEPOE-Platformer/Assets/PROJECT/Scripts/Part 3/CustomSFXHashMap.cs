using UnityEngine;

public class CustomSFXHashMap
{
    private int bucketCount;
    private HashNode[] buckets;

    public CustomSFXHashMap(int capacity = 16)
    {
        this.bucketCount = capacity;
        this.buckets = new HashNode[bucketCount];
    }

    // A custom Hash Function to turn a string key into an array index
    private int GetHashIndex(string key)
    {
        if (key == null) return 0;

        int hashCode = 0;
        // Simple polynomial rolling hash algorithm
        for (int i = 0; i < key.Length; i++)
        {
            hashCode = (hashCode * 31) + key[i];
        }

        // Ensure index is positive and fits inside our bucket array size
        int index = hashCode % bucketCount;
        return index < 0 ? index + bucketCount : index;
    }

    // Insert or update a key-value pair (The 'Add' operation)
    public void Put(string key, AudioClip value)
    {
        int index = GetHashIndex(key);
        HashNode head = buckets[index];

        // Check if the key already exists in the linked list chain to update it
        while (head != null)
        {
            if (head.Key == key)
            {
                head.Value = value; // Update existing sound clip
                return;
            }
            head = head.Next;
        }

        // Key doesn't exist, insert a new node at the head of the bucket chain
        HashNode newNode = new HashNode(key, value);
        newNode.Next = buckets[index];
        buckets[index] = newNode;
    }

    // Look up a value by its key (The 'Get' operation)
    public AudioClip Get(string key)
    {
        int index = GetHashIndex(key);
        HashNode head = buckets[index];

        // Traverse the specific bucket chain
        while (head != null)
        {
            if (head.Key == key)
            {
                return head.Value; // Found the clip!
            }
            head = head.Next;
        }

        // If not found, return null safely
        return null; 
    }
}