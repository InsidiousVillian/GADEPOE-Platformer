using UnityEngine;

public class Stack 
{
    private CheckpointData[] stackArray = new CheckpointData[10];
    private int topOfIndex = -1;

    public void Push(CheckpointData data)
    {
        // Ensure there's room in the stack before pushing new checkpoint data
        if (topOfIndex < stackArray.Length - 1)
        {
            // If so, push the new checkpoint data onto the stack
            topOfIndex++;
            stackArray[topOfIndex] = data; // Store the checkpoint data at the new top index
        }
        else
        {
            Debug.LogWarning("Stack is full! Cannot push new checkpoint.");
        }
    }
    
    // Pops the last checkpoint off the stack and returns it
    public CheckpointData Pop()
    {
        // Ensure there's a checkpoint to pop
        if (topOfIndex >= 0)
        {
            //if so, pop the last one
            CheckpointData data = stackArray[topOfIndex];
            stackArray[topOfIndex] = null; // Clear reference for garbage collection
            topOfIndex--; // Move down the stack
            return data; // Return the popped checkpoint data
        }
        else
        {
            Debug.LogWarning("Stack is empty! Cannot pop checkpoint.");
            return null;
        }
    }

    public CheckpointData Peek()
    {
        // Ensure there's a checkpoint to peek at
        if (topOfIndex >= 0)
        {
            // If so, return the checkpoint data at the top of the stack without modifying it
            return stackArray[topOfIndex];
        }
        else
        {
            Debug.LogWarning("Stack is empty! Cannot peek checkpoint.");
            return null;
        }
    }
}
