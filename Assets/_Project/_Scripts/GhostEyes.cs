using System;
using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Movement Movement { get; private set; }
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Movement = GetComponentInParent<Movement>();
    }

    private void Update()
    {
        if (Movement.Direction.Equals(Vector2.up))
            spriteRenderer.sprite = up;
        else if (Movement.Direction.Equals(Vector2.down))
            spriteRenderer.sprite = down;
        else if (Movement.Direction.Equals(Vector2.left))
            spriteRenderer.sprite = left;
        else if (Movement.Direction.Equals(Vector2.right))
            spriteRenderer.sprite = right;
    }
}
