using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostScatter : GhostBehavior
{
    private void OnDisable()
    {
        ghost.Chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Node node = col.GetComponent<Node>();

        if (node != null && enabled && !ghost.Frightened.enabled)
        {
            int index = Random.Range(0, node.AvailableDirections.Count);

            if (node.AvailableDirections[index] == -ghost.Movement.Direction && node.AvailableDirections.Count > 1)
            {
                index++;

                if (index >= node.AvailableDirections.Count)
                {
                    index = 0;
                }
            }
            
            ghost.Movement.SetDirection(node.AvailableDirections[index]);
        }
    }
}
