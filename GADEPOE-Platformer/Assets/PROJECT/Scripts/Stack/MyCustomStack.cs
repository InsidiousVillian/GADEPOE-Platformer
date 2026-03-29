using UnityEngine;

public class Stack : MonoBehaviour
{
    private CheckpointData[] stackArray = new CheckpointData[10];
    private int topOfIndex = -1;

    public void Push(CheckpointData data)
    {
        if (topOfIndex < stackArray.Length - 1)
        {
            topOfIndex++;
            stackArray[topOfIndex] = data;
        }
        else
        {
            Debug.LogWarning("Stack is full! Cannot push new checkpoint.");
        }
    }

    public CheckpointData Pop()
    {
        if (topOfIndex >= 0)
        {
            CheckpointData data = stackArray[topOfIndex];
            stackArray[topOfIndex] = null; // Clear reference for GC
            topOfIndex--;
            return data;
        }
        else
        {
            Debug.LogWarning("Stack is empty! Cannot pop checkpoint.");
            return null;
        }
    }

    public CheckpointData Peek()
    {
        if (topOfIndex >= 0)
        {
            return stackArray[topOfIndex];
        }
        else
        {
            Debug.LogWarning("Stack is empty! Cannot peek checkpoint.");
            return null;
        }
    }
}
