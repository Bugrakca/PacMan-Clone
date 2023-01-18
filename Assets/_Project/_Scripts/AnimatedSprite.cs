using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;
    public float animationTime = 0.25f;
    public int animationFrame { get; private set; }
    public bool loop = true;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Animate), animationTime, animationTime);
    }

    private void Animate()
    {
        if (!spriteRenderer.enabled)
        {
            return;
        }
        
        animationFrame++;

        if (animationFrame >= sprites.Length && loop)
        {
            animationFrame = 0;
        }

        if (animationFrame >= 0 && animationFrame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[animationFrame];
        }
    }

    public void Restart()
    {
        animationFrame = -1;
        Animate();
    }
}
