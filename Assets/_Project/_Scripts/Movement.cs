using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;
    public Vector2 initialDirection;
    public LayerMask wallLayer;
    public Vector2 Direction { get; private set; }
    public Vector2 NextDirection { get; private set; }
    public Vector3 StartingPosition { get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        StartingPosition = transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        if (NextDirection != Vector2.zero)
        {
            SetDirection(NextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = Rigidbody2D.position;
        Vector2 translation = Direction * (speed * speedMultiplier * Time.fixedDeltaTime);
        Rigidbody2D.MovePosition(position + translation);
    }

    public void ResetState()
    {
        speedMultiplier = 1.0f;
        Direction = initialDirection;
        NextDirection = Vector2.zero;
        transform.position = StartingPosition;
        Rigidbody2D.isKinematic = false;
        enabled = true;
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !Occupied(direction))
        {
            Direction = direction;
            NextDirection = Vector2.zero;
        }
        else
        {
            NextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit2D = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, wallLayer);
        return hit2D.collider != null;
    }
}