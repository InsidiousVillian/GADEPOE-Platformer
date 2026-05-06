using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float Speed;
    public Vector3 Size;
    public Color Appearance;

    // This method ensures every subclass sets its own stats
    public abstract void Initialize();

    protected void ApplyVisuals()
    {
        transform.localScale = Size;
        GetComponent<Renderer>().material.color = Appearance;
    }
}
