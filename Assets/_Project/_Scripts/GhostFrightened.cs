using System;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;
    
    public bool IsEaten { get; private set; }

    public override void Enable(float duration)
    {
        base.Enable(duration);
        body.enabled = false;
        eyes.enabled = false;
        blue.enabled = true;
        white.enabled = false;
        
        Invoke(nameof(Flash), duration / 2f);
    }

    public override void Disable()
    {
        base.Disable();
        
        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    private void Eaten()
    {
        IsEaten = true;
        ghost.SetPosition(ghost.Home.inside.position);
        ghost.Home.Enable(duration);
        
        body.enabled = false;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }
    
    private void Flash()
    {
        if (!IsEaten)
        {
            blue.enabled = false;
            white.enabled = true;
            white.GetComponent<AnimatedSprite>().Restart();
        }
    }

    private void OnEnable()
    {
        blue.GetComponent<AnimatedSprite>().Restart();
        ghost.Movement.speedMultiplier = 0.5f;
        IsEaten = false;
    }

    private void OnDisable()
    {
        ghost.Movement.speedMultiplier = 1f;
        IsEaten = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            // Find the available direction that moves farthest from pacman
            foreach (Vector2 availableDirection in node.AvailableDirections)
            {
                // If the distance in this direction is greater than the current
                // max distance then this direction becomes the new farthest
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            ghost.Movement.SetDirection(direction);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Pacman")))
        {
            if (enabled)
                Eaten();
        }
    }
}
