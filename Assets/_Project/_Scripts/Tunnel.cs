using System;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    [SerializeField]
    private Transform tunnelPosition;

    private void OnTriggerEnter2D(Collider2D col)
    {
        col.transform.position = tunnelPosition.position;
    }
}
