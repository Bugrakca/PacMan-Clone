using UnityEngine;
using Random = UnityEngine.Random;

public class GhostChase : GhostBehavior
{ 
    private void OnDisable()
    {
        ghost.Scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Node node = col.GetComponent<Node>();

        if (node != null && enabled && !ghost.Frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach (Vector2 availableDirection in node.AvailableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                var distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }
            
            ghost.Movement.SetDirection(direction);
        }
    }
}
