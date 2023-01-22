using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostHome : GhostBehavior
{
    public Transform inside;
    public Transform outside;
    private CircleCollider2D _collider2D;

    private void Start()
    {
        _collider2D = ghost.GetComponent<CircleCollider2D>();
        _collider2D.radius = 0.93f;
    }

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        _collider2D.radius = 0.5f;
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ExitTransition());
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (enabled && col.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            ghost.Movement.SetDirection(-ghost.Movement.Direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        ghost.Movement.SetDirection(Vector2.up, true);
        ghost.Movement.Rigidbody2D.isKinematic = true;
        ghost.Movement.enabled = false;

        Vector3 position = transform.position;

        float durationF = 0.5f;
        float elapsed = 0f;

        while (elapsed < durationF)
        {
            ghost.SetPosition(Vector3.Lerp(position, inside.position, elapsed / durationF));
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        while (elapsed < durationF)
        {
            ghost.SetPosition(Vector3.Lerp(inside.position, outside.position, elapsed / durationF));
            elapsed += Time.deltaTime;
            yield return null;

        }
        
        ghost.Movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0f), true);
        ghost.Movement.Rigidbody2D.isKinematic = false;
        ghost.Movement.enabled = true;
    }
}
